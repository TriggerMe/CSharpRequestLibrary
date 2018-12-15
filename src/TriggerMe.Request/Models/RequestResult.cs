namespace TriggerMe.Request.Models
{
    public enum RequestResult
    {
        // Request has started but not begun processing
        Started,

        // Request is queued up for Daemon operation
        WaitingForDaemon, 

        // Request is successful
        Successful, 

        // Request failed and is retrying
        Retrying, 

        // Request has failed
        Failed,

        // Request has been cancelled
        Cancelled
    }    
}