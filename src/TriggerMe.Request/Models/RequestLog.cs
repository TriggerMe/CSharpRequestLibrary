using System;

namespace TriggerMe.Request.Models
{
    /// <summary>
    /// Represents a request that is being processed by TriggerMe
    /// </summary>
    public class RequestLog
    {
        /// <summary>
        /// Unique ID for the request
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// The API Key that made this request (null for testing endpoint)
        /// </summary>
        public string ApiKeyId { get; set; }

        /// <summary>
        /// The Team that owns this request
        /// </summary>
        public string TeamId { get; set; }

        /// <summary>
        /// The Request Options
        /// </summary>
        public ForwardingOptions Forwarding { get; set; }

        /// <summary>
        /// The current retry count for this request
        /// </summary>
        /// <value></value>
        public int RetryCount { get; set; }

        /// <summary>
        /// Which IP Address made this request
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Incoming Request Data
        /// </summary>
        public RequestData Request { get; set; }

        /// <summary>
        /// The current result of the operation
        /// </summary>
        public RequestResult Result { get; set; }

        /// <summary>
        /// When the request was received
        /// </summary>
        /// <value></value>
        public DateTime Received { get; set; }

        /// <summary>
        /// The RetryRecords associated with this request (minimum 1)
        /// </summary>
        public RetryRecord[] RetryRecords { get; set; } = new RetryRecord[0];

        /// <summary>
        /// When the request was cancelled
        /// </summary>
        public DateTime? Cancelled { get; set; }

        /// <summary>
        /// When the request was completed
        /// </summary>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Has the request finished?
        /// </summary>
        public bool HasFinished => Completed != null || Cancelled  != null;
    }
}