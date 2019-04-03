using System;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Threading.Tasks;

namespace EventHubWriter
{
    class Program
    {

        private static EventHubClient eventHubClient;
        private const string EventHubConnectionString = "Endpoint=sb://tradens.servicebus.windows.net/;SharedAccessKeyName=tradehubPolicy;SharedAccessKey=Nrdy6DEHOrAkwYs9gGRSiswn6UuP5mtp/IFbMRj2yHM=;";
        private const string EventHubName = "tradehub";


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MainAsync().GetAwaiter().GetResult();

        }


        private static async Task MainAsync() { 

            var connection = new EventHubsConnectionStringBuilder(EventHubConnectionString)
            {
                  EntityPath = EventHubName
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connection.ToString());
            
            await SendMessageToHub();            
            await eventHubClient.CloseAsync();            
            Console.ReadLine();
        }

        private async static Task SendMessageToHub()
        {
            
           for (var i=0; i < 20; i++) {
            var message = "msg" + i;
            Console.WriteLine("sending data over");                        
            await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));

           }
        }
    }
}
