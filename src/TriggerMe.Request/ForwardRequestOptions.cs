namespace TriggerMe.Request
{
    /// <summary>
    /// Request options to be sent with a Forwarding request
    /// </summary>
    public class ForwardRequestOptions
    {
        /// <summary>
        /// Number of times to retry the request. Leaving as null will use default for your account
        /// </summary>
        public int? RetryCount { get; set; }

        /// <summary>
        /// If true, wait and return from the forwarding endpoint immediately. Overrides <see cref="RetryCount" />
        /// </summary>
        public bool ShouldPassthrough { get; set; }
    }
}