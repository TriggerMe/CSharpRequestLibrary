using System;
using System.Linq;
using System.Threading.Tasks;
using TriggerMe.Request.Models;

namespace TriggerMe.Request.ConsoleTest
{
    class Program
    {
        const string ApiKey = "[[INSERT YOUR TRIGGERME API KEY HERE]]";
        const string TargetUrl = "[[INSERT YOUR DESTINATION HERE]]";

        static async Task Main(string[] args)
        {
            Options.ApiKey = ApiKey;

            var client = new ForwardRequestClient();

            var response = await client.PostAsync(TargetUrl, null);

            Console.WriteLine(response.RequestId);

            var requestStatus = new ForwardRequestStatus();

            RequestLog update;

            do
            {
                await Task.Delay(1000);

                update = await requestStatus.CheckRequestAsync(response.RequestId);
                
                Console.WriteLine(update.Result);
            }
            while (!update.HasFinished);

            var request = await update.RetryRecords.Last().DownloadBlobAsStringAsync();

            Console.WriteLine(request);
        }
    }
}
