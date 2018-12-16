namespace TriggerMe.Request.Models
{
    /// <summary>
    /// Defines the current status of a TriggerMe request
    /// </summary>
    public enum RequestResult
    {
        /// <summary>Request has started but not begun processing</summary>
        Started,

        /// <summary>Request is queued up for Daemon operation</summary>
        WaitingForDaemon, 

        /// <summary>Request is successful</summary>
        Successful, 

        /// <summary>Request failed and is retrying</summary>
        Retrying, 

        /// <summary>Request has failed</summary>
        Failed,

        /// <summary>Request has been cancelled</summary>
        Cancelled
    }    
}