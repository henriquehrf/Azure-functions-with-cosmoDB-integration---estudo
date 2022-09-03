using Newtonsoft.Json;

namespace Task.Shared.Dtos
{
	public abstract class DtoBase
	{
		public DtoBase()
		{
			Id = Id ?? Guid.NewGuid();
		}

		[JsonProperty("id")]
		public Guid? Id { get; set; }
	}
}
