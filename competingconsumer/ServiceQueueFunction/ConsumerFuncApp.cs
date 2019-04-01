using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceQueueFunction
{
    public static class ConsumerFuncApp
    {
        [FunctionName("ConsumerFuncApp")]
        public static void Run([ServiceBusTrigger("tradequeue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
