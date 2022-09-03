using Newtonsoft.Json;

namespace Task.Shared.Dtos
{
	public class TaskDto : DtoBase
	{
		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("due")]
		public DateTime Due { get; set; }

		[JsonProperty("completed")]
		public bool Completed { get; set; }
	}
}
