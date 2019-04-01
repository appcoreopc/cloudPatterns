using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace Producer
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
            Console.WriteLine("Hello World!");
            queueClient = new QueueClient(ConnStr, TargetQueue);
            await SendMessageToQueue();
        }

        private static async Task SendMessageToQueue()
        {
                        
          for (var i=0; i < 100; i++) {

              var messageBody = "message value " + i;
              var msg = new Message(Encoding.UTF8.GetBytes(messageBody));
              await queueClient.SendAsync(msg);
          }
        }
    }
}
