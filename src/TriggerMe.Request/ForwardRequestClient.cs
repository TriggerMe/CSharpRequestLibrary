using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TriggerMe.Request.Models;

namespace TriggerMe.Request
{
    public class ForwardRequestClient
    {
        static HttpClient _httpClient = new HttpClient();

        public static string Endpoint => Options.Endpoint + "/f";

        public Task<ForwardResponse> PostAsync(string url, HttpContent httpContent)
        {
            return PostAsync(url, httpContent, null);
        }

        public Task<ForwardResponse> PostAsync(string url, HttpContent httpContent, ForwardRequestOptions options)
        {
            return SendAsync(url, HttpMethod.Post, null, httpContent, options);
        }

        public Task<ForwardResponse> PutAsync(string url, HttpContent httpContent)
        {
            return PutAsync(url, httpContent, null);
        }

        public Task<ForwardResponse> PutAsync(string url, HttpContent httpContent, ForwardRequestOptions options)
        {
            return SendAsync(url, HttpMethod.Put, null, httpContent, options);
        }
        
        public Task<ForwardResponse> DeleteAsync(string url)
        {
            return DeleteAsync(url, null);
        }

        public Task<ForwardResponse> DeleteAsync(string url, ForwardRequestOptions options)
        {
            return SendAsync(url, HttpMethod.Delete, null, null, options);
        }

        public async Task<ForwardResponse> SendAsync(string url, HttpMethod method, Dictionary<string, string> extraHeaders, HttpContent httpContent, ForwardRequestOptions options)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (options == null)
                options = new ForwardRequestOptions();

            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri(Endpoint);

                requestMessage.Method = HttpMethod.Post;

                if (httpContent != null)
                    requestMessage.Content = httpContent;

                if (extraHeaders != null)
                {
                    foreach (var header in extraHeaders)
                    {
                        requestMessage.Headers.Add(header.Key, header.Value);
                    }
                }

                requestMessage.Headers.Add("TME-ApiKey", Options.ApiKey);
                requestMessage.Headers.Add("TME-Url", url);

                if (options.ShouldPassthrough)
                {
                    requestMessage.Headers.Add("TME-Passthrough", "true");
                }
                else if (options.RetryCount != null)
                {
                    requestMessage.Headers.Add("TME-Retry", options.RetryCount.Value.ToString(CultureInfo.InvariantCulture.NumberFormat));
                }
                
                using (var response = await _httpClient.SendAsync(requestMessage))
                {
                    var respStr = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        var requestError = JsonConvert.DeserializeObject<RequestError>(respStr);
                        throw new ForwardRequestException(requestError.Message);
                    }

                    return JsonConvert.DeserializeObject<ForwardResponse>(respStr);
                }
            }
        }
    }
}
