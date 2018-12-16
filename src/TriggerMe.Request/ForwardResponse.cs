namespace TriggerMe.Request
{
    /// <summary>
    /// Represents a response from a Forwarding request
    /// </summary>
    public class ForwardResponse
    {
        /// <summary>
        /// Id of the request (used for status calls)
        /// </summary>
        public string RequestId { get; set; }
    }
}