using BLL.Communication;
using BLL.External;
using BLL.Helpers;
using BO.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PMCommunicator : IPMCommunicator
    {
        /// <summary>
        /// The supported device names used to search for Perofrmance Monitors
        /// </summary>
        private static readonly IEnumerable<string> _supportedDeviceNames = new[]
        {
            "Concept2 Performance Monitor 3 (PM3)",
            "Concept2 Performance Monitor 5 (PM5)"
        };

        private static readonly PortMonitor _portMonitor = new PortMonitor();

        private readonly ILogger<PMCommunicator> _logger;
        private readonly IExceptionActivator _exceptionActivator;

        public PMCommunicator(IExceptionActivator exceptionActivator, ILogger<PMCommunicator> logger)
        {
            _logger = logger;
            _exceptionActivator = exceptionActivator;
        }

        /// <inheritdoc/>
        public void InitializeCSafe()
        {
            try 
            { 
                PM3CSAFE.CSAFE_InitializeProtocol(1000);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred while initializing the CSAFE Protocol.");
                throw;
            }
        }

        /// <inheritdoc/>
        public void InitializeDDI()
        {
            try
            {
                PM3DDI.DDI_Initialize();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred while initializing the DDI Protocol.");
                throw;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<ushort> DiscoverPorts()
        {
            ushort totalNumberOfUnits = 0;

            foreach (string deviceName in _supportedDeviceNames)
            {
                try 
                { 
                    short errorCode = PM3DDI.DDI_Discover(deviceName, 0, out ushort numberOfUnits);
                    EnsureSuccess(errorCode);

                    totalNumberOfUnits += numberOfUnits;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An exception occurred while discovering PMs named [{PMName}]. Continuting...", deviceName);
                }
            }

            return Enumerable.Range(0, totalNumberOfUnits).Select(unitNumber => (ushort)unitNumber);
        }

        /// <inheritdoc/>
        public string GetSerialNumber(ushort port)
        {
            StringBuilder serialNumber = new StringBuilder(16);
            short errorCode = PM3DDI.DDI_SerialNumber(port, serialNumber, (byte)(serialNumber.Capacity + 1));
            EnsureSuccess(errorCode);

            return serialNumber.ToString();
        }

        /// <inheritdoc/>
        public void Send(ushort port, ICommandList commands)
        {
            if (!commands.CanSend)
            {
                Exception e = new InvalidOperationException("CommandList was not made ready before Send was called");
                _logger.LogError(e, "Send failed.");
                throw e;
            }

            lock (_portMonitor[port]) 
            {
                bool shouldRetry = false;
                ushort attemptCount = 1;

                do
                {
                    try
                    {
                        // Get the expected response size and set the response reader
                        ushort responseDataSize = commands.ExpectedResponseSize;
                        ResponseReader responseReader = new ResponseReader(responseDataSize);

                        // Execute the send
                        Send(port, commands.Buffer, commands.Size, responseReader.Buffer, ref responseDataSize);

                        // Reset the reader to the correct length based on returned value
                        responseReader.Resize(responseDataSize);

                        // Read the response into the commands
                        bool success = commands.Read(responseReader);

                        if (!success)
                        {
                            _logger.LogError("The response was read, but the buffer was not fully consumed on attempt # [{AttemptNumber}]", attemptCount);
                            shouldRetry = ++attemptCount <= 3;
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "An exception occurred while sending/reading data to/from the PM on attempt # [{AttemptNumber}]", attemptCount);

                        // Retry for a total of 3 attempts
                        shouldRetry = ++attemptCount <= 3;
                    }
                } while (shouldRetry);
            }
        }

        /// <summary>
        /// Send a command data to the Performance Monitor
        /// </summary>
        /// <param name="commandData">The command data</param>
        /// <param name="commandDataSize">The size of the command data</param>
        /// <param name="responseData">The response data</param>
        /// <param name="responseDataSize">The size of the response data</param>
        private void Send(ushort port, uint[] commandData, int commandDataSize, uint[] responseData, ref ushort responseDataSize)
        {
            short errorCode = PM3CSAFE.CSAFE_Command(port, (ushort)commandDataSize, commandData, ref responseDataSize, responseData);

            EnsureSuccess(errorCode);
        }

        private void EnsureSuccess(short errorCode)
        {
            if (errorCode == 0)
            {
                // If no error code is returned from the PM, the commands were successful
                return;
            }

            // Creates an exception based on the error code and throws it
            _exceptionActivator.CreateException(errorCode);
        }
    }
}
