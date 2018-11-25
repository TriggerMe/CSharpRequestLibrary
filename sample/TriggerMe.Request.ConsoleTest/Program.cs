using System;
using System.Threading.Tasks;

namespace TriggerMe.Request.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ForwardRequestClient.ApiKey = "[[INSERT YOUR TRIGGERME API KEY HERE]]";

            var client = new ForwardRequestClient();

            var response = await client.PostAsync("http://www.google.com", null);

            Console.WriteLine(response.RequestId);
        }
    }
}
