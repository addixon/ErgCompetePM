using PM.BO;
using PM.BO.Interfaces;
using System;
using System.Reflection;

namespace BLL.Extensions
{
    /// <summary>
    /// Extensions for ICommands
    /// </summary>
    public static class ICommandExtensions
    {
        /// <summary>
        /// Updates the cooresponding property in PMData with the value of the command
        /// </summary>
        /// <param name="command">The command</param>
        /// <param name="pmData">The PMData object to update</param>
        public static void UpdatePMData(this ICommand command, PMData pmData)
        {
            if (pmData == null)
            {
                return;
            }

            if (command.Value == null)
            {
                return;
            }

            PropertyInfo? property = pmData.GetType().GetProperty(command.Name);

            if (property == null)
            {
                return;
            }

            if (Nullable.GetUnderlyingType(property.PropertyType) != command.Value.GetType())
            {
                return;
            }

            property.SetValue(pmData, command.Value);
        }
    }
}