using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TriggerMe.Request
{
    public class ForwardRequestClient
    {
        static HttpClient _httpClient = new HttpClient();

        public static string ApiKey { get; set; }

        public static string TriggerMeEndpoint { get; set; } = Constants.DefaultEndpoint;

        public Task<ForwardResponse> PostAsync(string url, HttpContent httpContent)
        {
            return PostAsync(url, httpContent, null);
        }

        public Task<ForwardResponse> PostAsync(string url, HttpContent httpContent, ForwardRequestOptions options)
        {
            return SendAsync(url, HttpMethod.Post, httpContent, options);
        }

        public async Task<ForwardResponse> SendAsync(string url, HttpMethod method, HttpContent httpContent, ForwardRequestOptions options)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (options == null)
                options = new ForwardRequestOptions();

            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri(TriggerMeEndpoint);

                requestMessage.Method = HttpMethod.Post;

                requestMessage.Headers.Add("TME-ApiKey", ApiKey);
                requestMessage.Headers.Add("TME-Url", url);
                
                using (var response = await _httpClient.SendAsync(requestMessage))
                {
                    var respStr = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ForwardResponse>(respStr);
                }
            }
        }
    }
}
