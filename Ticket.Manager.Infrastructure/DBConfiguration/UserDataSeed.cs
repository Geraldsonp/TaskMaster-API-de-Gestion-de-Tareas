using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Manager.Infrastructure.DBConfiguration;

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
        builder.Entity<User>().HasData(new[]
        {
            new User()
            {
                FullName = "John Doe",
                Id = 2,
                IdentityId = id2,
            }
        });
    }
}