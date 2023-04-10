using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TaskMaster.Infrastructure.DBConfiguration;

public static class UserDataSeed
{

	public static void SeedDb(this ModelBuilder builder)
	{
		string id2 = new Guid().ToString();
		builder.Entity<IdentityUser>().HasData(new[]
		{
			new IdentityUser()
			{
				UserName = "JohnDoe",
				Email = "JhonDoe@gmail.com",
				Id = id2
			},
		});
	}
}