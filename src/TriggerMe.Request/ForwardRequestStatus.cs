using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TriggerMe.Request.Models;

namespace TriggerMe.Request
{
    /// <summary>
    /// Helper class to check TriggerMe for the status of a Forwarding request
    /// </summary>
    public class ForwardRequestStatus
    {
        static HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Endpoint to check status.
        /// For on-premise installations, override <see cref="Options.Endpoint" />
        /// </summary>
        public static string LogEndpoint => Options.Endpoint + "/l";

        /// <summary>
        /// Creates a new ForwardRequestStatus object
        /// </summary>
        public ForwardRequestStatus()
        {
            // Ignore
        }

        /// <summary>
        /// Checks the status of a request
        /// </summary>
        /// <param name="requestId">Request Id to check</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="requestId" /> is null</exception>
        /// <returns>A valid RequestLog if successful</returns>
        public async Task<RequestLog> CheckRequestAsync(string requestId)
        {
            if (string.IsNullOrEmpty(requestId))
                throw new ArgumentNullException(nameof(requestId));

            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(LogEndpoint + "/" + requestId);

                request.Headers.Add("TME-ApiKey", Options.ApiKey);

                using (var response = await _httpClient.SendAsync(request))
                {
                    var respStr = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        var requestError = JsonConvert.DeserializeObject<RequestError>(respStr);
                        throw new ForwardRequestException(requestError.Message);
                    }

                    var requestLog = JsonConvert.DeserializeObject<RequestLog>(respStr);

                    return requestLog;
                }
            }
        }
    }
}