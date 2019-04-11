using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;

namespace AzFunctionCore
{
    public static class VotingFunction
    {
        [FunctionName("VotingFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {          
            string name = req.Query["name"];

            log.LogInformation($"variable name {name}");
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            if (name != null) { 
                string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=votingstore;AccountKey=VOTqH0bXD2clXhpgcBCCfRmL8aDrvWD78BoBP4Zm1XhpW3yuTKZ7uY1BWJWtu53fFuCe58zcZPUl5KpPs5Azbw==;EndpointSuffix=core.windows.net";
                CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
               
                log.LogInformation("trying to connect to client ");
                
                // CloudBlobClient serviceClient = account.CreateCloudBlobClient();
                // var container = serviceClient.GetContainerReference("mycontainer");
                // container.CreateIfNotExistsAsync().Wait();   

                var tableClient = account.CreateCloudTableClient();
                var targetTable = tableClient.GetTableReference("people");
                targetTable.CreateIfNotExistsAsync().Wait();

                var p = new People("jeremy", "woo") { Id = 1, Name = "jerwo"};
                var insertOperation = TableOperation.Insert(p);
                targetTable.ExecuteAsync(insertOperation);

                log.LogInformation("Done!.");              

                TableQuery<People> query = new TableQuery<People>().Take(10);

                var result = await targetTable.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());

                log.LogInformation($"total record retrieved {result.Results.ToArray().Length}");
                

            }
        
            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }


   public class People : TableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public People(string lastName, string firstName)
    {
        PartitionKey = lastName;
        RowKey = firstName;
    }

    public People() { }
}
}
