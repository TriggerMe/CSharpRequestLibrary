using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TriggerMe.Request.Models;

namespace TriggerMe.Request
{
    public class ForwardRequestStatus
    {
        static HttpClient _httpClient = new HttpClient();

        static string LogEndpoint => Options.Endpoint + "/l";

        public ForwardRequestStatus()
        {

        }

        public async Task<RequestLog> CheckRequestAsync(string requestId)
        {
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