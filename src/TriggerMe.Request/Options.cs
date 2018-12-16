namespace TriggerMe.Request
{
    /// <summary>
    /// Options for TriggerMe
    /// </summary>
    public static class Options
    {
        private const string DefaultEndpoint = "https://run.triggerme.io";

        /// <summary>
        /// Endpoint of the TriggerMe Request Runner. 
        /// By default, this is set to the TriggerMe service, change for On-Premise installations
        /// </summary>
        public static string Endpoint { get; set; } = DefaultEndpoint;

        /// <summary>
        /// Your TriggerMe API key
        /// </summary>
        public static string ApiKey { get; set; }
    }
}