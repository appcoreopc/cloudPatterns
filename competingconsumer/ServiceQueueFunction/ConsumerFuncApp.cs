using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceQueueFunction
{
    public static class ConsumerFuncApp
    {
        [FunctionName("ConsumerFuncApp")]
        public static void Run([ServiceBusTrigger("tradequeue", Connection = "Endpoint=sb://sbservice550.servicebus.windows.net/;SharedAccessKeyName=accesstradequeue;SharedAccessKey=p1TJpen9vgB7RthWBwNB7OrnV3/rwW3FhlyQ9/2K54g=;")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
