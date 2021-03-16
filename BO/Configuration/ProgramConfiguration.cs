namespace PM.BO.Configuration
{
    /// <summary>
    /// Configuration for the program
    /// </summary>
    public class ProgramConfiguration
    {
        /// <summary>
        /// The url of the SignalR Hub which listens for Erg data
        /// </summary>
        public string? SignalRHubEndpoint { get; set; }
    }
}
