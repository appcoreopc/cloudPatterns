using System;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ServiceBus;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AzMgt
{
    class Program
    {
        static string tokenValue = string.Empty;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }


        static async Task Create() {

            await CreateNamespace().ConfigureAwait(false);
        }

        private static Task CreateNamespace()
        {
            
        }


        private async static Task<string> GetToken() {


            var ctx = new AuthenticationContext($"ttps://login.microsoftonline.com/{tenantId}");

            var result = await ctx.AcquireTokenAsync
            (
                "https://management.core.windows.net/", 
                new ClientCredential("", "")
            );

            if (!string.IsNullOrWhiteSpace(result.AccessToken)) {

            }

            tokenValue = result.AccessToken;
            return tokenValue;
        }
    }
}
