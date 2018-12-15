using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TriggerMe.Request.Models;

namespace TriggerMe.Request
{
    public static class BodyBlobExtensions
    {
        static HttpClient _httpClient = new HttpClient();

        public static Task<HttpResponseMessage> DownloadBlobAsync(this IBodyBlob bodyBlob)
        {
            if (string.IsNullOrEmpty(bodyBlob?.BlobUri))
            {
                throw new ArgumentNullException();
            }

            return _httpClient.GetAsync(bodyBlob.BlobUri);
        }

        public static async Task<string> DownloadBlobAsStringAsync(this IBodyBlob bodyBlob)
        {
            var responseMessage = await DownloadBlobAsync(bodyBlob);

            var respStr = await responseMessage.Content.ReadAsStringAsync();

            return respStr;
        }

        public static async Task<T> DownloadBlobAsync<T>(this IBodyBlob bodyBlob)
        {
            var respStr = await DownloadBlobAsStringAsync(bodyBlob);

            return JsonConvert.DeserializeObject<T>(respStr);
        }
    }
}