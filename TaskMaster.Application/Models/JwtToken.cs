namespace TaskMaster.Application.Models
{
	public class JwtToken
	{
		public DateTime ValidTo { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Token { get; set; }
	}
}