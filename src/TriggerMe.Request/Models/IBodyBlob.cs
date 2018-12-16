namespace TriggerMe.Request.Models
{
    /// <summary>
    /// Implemented by classes that contain a URI for a Request/Response Body
    /// </summary>
    public interface IBodyBlob
    {
        /// <summary>
        /// URI for the request/response blob
        /// </summary>
        /// <value></value>
        string BlobUri { get; }
    }
}