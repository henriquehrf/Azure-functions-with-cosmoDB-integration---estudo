using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Task.Shared.Dtos;
using System.Collections.Generic;

namespace TaskTrigger
{
    public static class TaskRepository
    {
        [FunctionName("TaskRepository")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
			[CosmosDB(databaseName: "taskDB", collectionName: "task",
					  ConnectionStringSetting = "CosmosDbConnectionString",
                      SqlQuery = "select * from task")] IEnumerable<TaskDto> taskDtos,
			ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(taskDtos);
        }
    }
}
