namespace TriggerMe.Request
{
    public static class Options
    {
        private const string DefaultEndpoint = "https://run.triggerme.io";

        public static string Endpoint { get; set; } = DefaultEndpoint;

        public static string ApiKey { get; set; }
    }
}