using System;
using System.Collections.Generic;

namespace TriggerMe.Request.Models
{
    /// <summary>
    /// Defines a TriggerMe Request Attempt
    /// </summary>
    public class RetryRecord : IBodyBlob
    {
        /// <summary>
        /// When the Retry started
        /// </summary>
        /// <value></value>
        public DateTime Started { get; set; }

        /// <summary>
        /// ms between starting and accepting
        /// </summary>
        public long WaitTime { get; set; }

        /// <summary>
        /// Response Status
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Any request error associated with the attempt
        /// </summary>
        public string RequestError { get; set; }

        /// <summary>
        /// Headers returned from the response
        /// </summary>
        public Dictionary<string, string[]> ResponseHeaders { get; set; }

        /// <summary>
        /// Response Body location within Blob Storage
        /// </summary>
        /// <value></value>
        public string BlobUri { get; set; }

        /// <summary>
        /// Content-Type of the response
        /// </summary>
        /// <value></value>
        public string ContentType { get; set; }

        /// <summary>
        /// Content-Length of the response
        /// </summary>
        /// <value></value>
        public long ContentLength { get; set; }
    }
}