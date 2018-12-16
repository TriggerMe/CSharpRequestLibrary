using System;
using System.Runtime.Serialization;

namespace TriggerMe.Request
{
    /// <summary>
    /// Exception object thrown when errors occur during Request Forwarding
    /// </summary>
    public class ForwardRequestException : ApplicationException
    {
        /// <summary>
        /// Creates a new ForwardRequestException
        /// </summary>
        /// <param name="message">Message received from TriggerMe service</param>
        public ForwardRequestException(string message) : base(message)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected ForwardRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}