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

namespace Henrique.dev
{
	public static class TaskTrigger
	{
		[FunctionName("TaskTrigger")]
		public static async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
			[CosmosDB(databaseName: "taskDB", collectionName: "task",
					  ConnectionStringSetting = "CosmosDbConnectionString", CreateIfNotExists=true)]IAsyncCollector<TaskDto> documentsOut,
			ILogger log)
		{
			log.LogInformation("C# HTTP TaskTrigger function processed a request.");

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

			if (string.IsNullOrWhiteSpace(requestBody))
				return new BadRequestObjectResult("Task is required");


			var data = JsonConvert.DeserializeObject<TaskDto>(requestBody);
			await documentsOut.AddAsync(data);

			log.LogInformation($"Data persisted with unique identifier {data.Id}");

			return new OkObjectResult(data);
		}
	}
}
