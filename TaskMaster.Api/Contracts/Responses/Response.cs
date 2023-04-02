namespace TaskMaster.Api.Contracts.Responses
{
	public class Response<T>
	{
		public bool IsSucess { get; set; }
		public string? ErrorMessage { get; set; }
		public T? Data { get; set; }

		public Response()
		{

		}

		public Response(T data)
		{
			Data = data;
			IsSucess = true;
		}

		public Response(string errorMessage)
		{
			ErrorMessage = errorMessage;
			IsSucess = false;
		}
	}
}