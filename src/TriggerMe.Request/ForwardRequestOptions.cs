namespace TriggerMe.Request
{
    public class ForwardRequestOptions
    {
        public int? RetryCount { get; set; }

        public bool ShouldPassthrough { get; set; }
    }
}