

using Azure;

using Azure.Data.Tables;

using Azure.Data.Tables.Models;

using Microsoft.Azure.Cosmos.Table;

using Microsoft.Azure.Documents;

using System;

using System.Collections.Concurrent;

using System.Threading.Tasks;

 

namespace AzureTablesApp

{

    class Program

    {

        static async Task Main(string[] args)

        {

           

            Console.WriteLine("Hello Tables!");

 

            var tableClient = new TableClient(GetConnString(), tableName: "Customers");

 

 

            var queryResultsLINQ = tableClient.Query<CustomerEntity2>(x => x.RowKey == "Zenek");

 

            //await InsertEntity();

 

        }

 

 

        public class CustomerEntity2 : Azure.Data.Tables.ITableEntity

        {

            public string Email { get; set; }

            public string PhoneNumber { get; set; }

            public string PartitionKey { get; set; }

            public string RowKey { get; set; }

            public DateTimeOffset? Timestamp { get; set; }

            public ETag ETag { get; set; }

        }

 

        private static async Task InsertEntity()

        {

            string storageConnectionString = GetConnString();

            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            var table = tableClient.GetTableReference("Customers");

            await table.CreateIfNotExistsAsync();

 

            var entity = new CustomerEntity("Kowalski", "Zenek");

 

            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

 

            // Execute the operation.

            TableResult result = await table.ExecuteAsync(insertOrMergeOperation);

            CustomerEntity insertedCustomer = result.Result as CustomerEntity;

        }

 

        static string GetConnString()

        {

            return "DefaultEndpointsProtocol=https;AccountName=storagebk2;AccountKey=INZNr+dazWF/6YpgimSvRxj8yeUxvHGxBa8DTiZ8Acl+oxXhhaVcyHdpPnAzbh58zU7TxC1yiMQLF3Sh7ElAdw==;EndpointSuffix=core.windows.net";

        }

    }

}
