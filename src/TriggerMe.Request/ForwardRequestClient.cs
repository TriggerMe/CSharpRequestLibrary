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
    /// <summary>
    /// Helper class to forward requests to TriggerMe
    /// </summary>
    public class ForwardRequestClient
    {
        static HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Endpoint to send requests to.
        /// For on-premise installations, override <see cref="Options.Endpoint" />
        /// </summary>
        public static string Endpoint => Options.Endpoint + "/f";

        /// <summary>
        /// Sends a POST request
        /// </summary>
        /// <param name="url">Destination URL</param>
        /// <param name="httpContent">Content to send to the URL (can be null)</param>
        /// <returns>Return object containing the RequestId</returns>
        public Task<ForwardResponse> PostAsync(string url, HttpContent httpContent)
        {
            return PostAsync(url, httpContent, null);
        }

        /// <summary>
        /// Sends a POST request
        /// </summary>
        /// <param name="url">Destination URL</param>
        /// <param name="httpContent">Content to send to the URL (can be null)</param>
        /// <param name="options">Extra options to use (can be null)</param>
        /// <returns>Return object containing the RequestId</returns>
        public Task<ForwardResponse> PostAsync(string url, HttpContent httpContent, ForwardRequestOptions options)
        {
            return SendAsync(url, HttpMethod.Post, null, httpContent, options);
        }

        /// <summary>
        /// Sends a PUT request
        /// </summary>
        /// <param name="url">Destination URL</param>
        /// <param name="httpContent">Content to send to the URL (can be null)</param>
        /// <returns>Return object containing the RequestId</returns>
        public Task<ForwardResponse> PutAsync(string url, HttpContent httpContent)
        {
            return PutAsync(url, httpContent, null);
        }

        /// <summary>
        /// Sends a PUT request
        /// </summary>
        /// <param name="url">Destination URL</param>
        /// <param name="httpContent">Content to send to the URL (can be null)</param>
        /// <param name="options">Extra options to use (can be null)</param>
        /// <returns>Return object containing the RequestId</returns>
        public Task<ForwardResponse> PutAsync(string url, HttpContent httpContent, ForwardRequestOptions options)
        {
            return SendAsync(url, HttpMethod.Put, null, httpContent, options);
        }
        
        /// <summary>
        /// Sends a DELETE request
        /// </summary>
        /// <param name="url">Destination URL</param>
        /// <returns>Return object containing the RequestId</returns>
        public Task<ForwardResponse> DeleteAsync(string url)
        {
            return DeleteAsync(url, null);
        }

        /// <summary>
        /// Sends a DELETE request
        /// </summary>
        /// <param name="url">Destination URL</param>
        /// <param name="options">Extra options to use (can be null)</param>
        /// <returns>Return object containing the RequestId</returns>
        public Task<ForwardResponse> DeleteAsync(string url, ForwardRequestOptions options)
        {
            return SendAsync(url, HttpMethod.Delete, null, null, options);
        }

        /// <summary>
        /// Sends a request for Forwarding
        /// </summary>
        /// <param name="url">Destination URL</param>
        /// <param name="extraHeaders">Extra headers to send to the URL (can be null)</param>
        /// <param name="method">Http Method to use</param>
        /// <param name="httpContent">Content to send to the URL (can be null)</param>
        /// <param name="options">Extra options to use (can be null)</param>
        /// <exception cref="ForwardRequestException">Thrown if the Request fails</exception>
        /// <returns>Return object containing the RequestId</returns>
        public async Task<ForwardResponse> SendAsync(string url, HttpMethod method, Dictionary<string, string> extraHeaders, HttpContent httpContent, ForwardRequestOptions options)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrEmpty(Options.ApiKey))
                throw new ForwardRequestException("Missing API Key");

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
