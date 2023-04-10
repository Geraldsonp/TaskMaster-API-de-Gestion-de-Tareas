namespace TaskMaster.Api.Contracts.Responses
{
	public class PaginationResponse<T>
	{
		public Response<T>? Response { get; set; }
		public int TotalPages { get; set; }
	}
}