using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System;   
using Microsoft.Azure.Management.ResourceManager;

namespace TopicProducer
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://sbservice550.servicebus.windows.net/;SharedAccessKeyName=TradeTopicPolicy;SharedAccessKey=S7jZdIxUrjNuOb9lVQ6WvRLAE7EK0CutiWgn9QEdPy0=;";
        const string TopicName = "tradetopic";

        static ITopicClient client;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MainAsync().GetAwaiter().GetResult();

        }

        static void CreteTopic() {      
         
        }

        static async Task MainAsync()
        {
            client = new TopicClient(ServiceBusConnectionString, TopicName);
            
                // Send messagee //
            await SendMessagesAsync();
            System.Console.ReadKey();
            await client.CloseAsync();
        }

        private static async Task SendMessagesAsync() {
           
           for (var i = 0; i < 20; i++ ) {

            var content = "Tradedata" + i;
            var message = new Message(Encoding.UTF8.GetBytes(content));
            message.SessionId = Guid.NewGuid().ToString();

            await client.SendAsync(message);
           }                
        }
    }
}




