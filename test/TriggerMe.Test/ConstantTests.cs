using System;
using Xunit;
using TriggerMe.Request;

namespace TriggerMe.Test
{
    public class ConstantsTest
    {
        [Fact]
        public void EndpointIsValid()
        {
            Assert.Equal("https://run.triggerme.io/f", Constants.DefaultEndpoint);
        }
    }
}
