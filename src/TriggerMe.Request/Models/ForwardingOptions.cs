namespace TriggerMe.Request.Models
{
    public class ForwardingOptions
    {
        /// <summary>
        /// The URL to Forward to
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// How many times this request should retry
        /// </summary>
        public int MaxRetryCount { get; set; }

        /// <summary>
        /// If true, the response of the request should be passed back to the caller (no retries allowed)
        /// </summary>
        public bool Passthrough { get; set; }
    }
}