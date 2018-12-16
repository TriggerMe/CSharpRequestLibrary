using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TriggerMe.Request.Models;

namespace TriggerMe.Request
{
    /// <summary>
    /// Extension methods for downloading blobs from Requests/Responses
    /// </summary>
    public static class BodyBlobExtensions
    {
        static HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Downloads the blob from the IBodyBlob
        /// </summary>
        /// <param name="bodyBlob">IBodyBlob object containing the blob to download</param>
        /// <returns>Standard HttpResponseMessage for manual parsing</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when BlobUri is null or empty</exception>
        public static Task<HttpResponseMessage> DownloadBlobAsync(this IBodyBlob bodyBlob)
        {
            if (string.IsNullOrEmpty(bodyBlob?.BlobUri))
            {
                throw new ArgumentNullException();
            }

            return _httpClient.GetAsync(bodyBlob.BlobUri);
        }

        /// <summary>
        /// Downloads the blob from IBodyBlob as a string
        /// </summary>
        /// <param name="bodyBlob">IBodyBlob object containing the blob to download</param>
        /// <returns>String result from the Blob</returns>
        public static async Task<string> DownloadBlobAsStringAsync(this IBodyBlob bodyBlob)
        {
            var responseMessage = await DownloadBlobAsync(bodyBlob);

            var respStr = await responseMessage.Content.ReadAsStringAsync();

            return respStr;
        }

        /// <summary>
        /// Downloads the blob from IBodyBlob and converts the returned Json into the specified class
        /// </summary>
        /// <param name="bodyBlob">IBodyBlob object containing the blob to download</param>
        /// <typeparam name="T">Type to convert the Json to</typeparam>
        public static async Task<T> DownloadBlobAsync<T>(this IBodyBlob bodyBlob)
        {
            var respStr = await DownloadBlobAsStringAsync(bodyBlob);

            return JsonConvert.DeserializeObject<T>(respStr);
        }
    }
}