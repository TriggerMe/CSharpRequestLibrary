using System;
using System.Runtime.Serialization;

namespace TriggerMe.Request
{
    public class ForwardRequestException : ApplicationException
    {
        public ForwardRequestException(string message) : base(message)
        {

        }

        protected ForwardRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}