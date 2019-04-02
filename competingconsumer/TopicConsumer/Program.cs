using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System;

namespace TopicConsumer
{
    class Program
    { 
        
        const string ServiceBusConnectionString = "Endpoint=sb://sbservice550.servicebus.windows.net/;SharedAccessKeyName=TradeTopicPolicy;SharedAccessKey=S7jZdIxUrjNuOb9lVQ6WvRLAE7EK0CutiWgn9QEdPy0=;";
        const string TopicSubscription = "tradesubscription";
        static ISubscriptionClient tradeTopicClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MainAsync().GetAwaiter().GetResult();
            //MainAsync();
        }

        async static Task MainAsync() {  

      tradeTopicClient = new SubscriptionClient(ServiceBusConnectionString, "tradetopic", TopicSubscription);

     
         RegisterMessagesHandlerAsync();          
            
  
         Console.ReadKey();

         await tradeTopicClient.CloseAsync();

        }

        private static void RegisterMessagesHandlerAsync()
        {
            var option = new MessageHandlerOptions(ExceptionReceivedHandler) {

               MaxConcurrentCalls = 1, 
               AutoComplete = false,
            };

            tradeTopicClient.RegisterMessageHandler(async (msg, c) => {
                Console.WriteLine($"Message received : {Encoding.UTF8.GetString(msg.Body)}");
                await tradeTopicClient.CompleteAsync(msg.SystemProperties.LockToken
            );            
            }, option);
            
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
