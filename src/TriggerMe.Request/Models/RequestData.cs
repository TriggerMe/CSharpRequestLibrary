using System.Collections.Generic;

namespace TriggerMe.Request.Models
{
    /// <summary>
    /// Represents the Request Data that is sent for a TriggerMe Request
    /// </summary>
    public class RequestData : IBodyBlob
    {
        /// <summary>
        /// Full Request path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Dictionary of all incoming headers
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Incoming Http Method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Full URI of the Blob
        /// </summary>
        /// <value></value>
        public string BlobUri { get; set; }

        /// <summary>
        /// Content-Type of the incoming request
        /// </summary>
        /// <value></value>
        public string ContentType { get; set; }

        /// <summary>
        /// Size of the content
        /// </summary>
        /// <value></value>
        public long? ContentLength { get; set; }
    }
}