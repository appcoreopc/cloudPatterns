using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Consumer
{
    class Program
    {        
         const string ConnStr = "Endpoint=sb://sbservice550.servicebus.windows.net/;SharedAccessKeyName=accesstradequeue;SharedAccessKey=p1TJpen9vgB7RthWBwNB7OrnV3/rwW3FhlyQ9/2K54g=;";
        const string TargetQueue = "tradequeue";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {           
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
       {
           
            queueClient = new QueueClient(ConnStr, TargetQueue);

            SetupMessageHandler();
                   
            Console.ReadKey();
            
            await queueClient.CloseAsync();                               
       }

       private static void SetupMessageHandler() { 

           var options = new MessageHandlerOptions(ExceptionReceivedHandler) 
           {
                  MaxConcurrentCalls = 1,
                  AutoComplete = false                  
           };

           queueClient.RegisterMessageHandler(ProcessMessagesAsync, options);

       }

       static async Task ProcessMessagesAsync(Message message, CancellationToken cancel) {

            Console.WriteLine($"Body:{Encoding.UTF8.GetString(message.Body)}");
           await queueClient.CompleteAsync(message.SystemProperties.LockToken);
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
