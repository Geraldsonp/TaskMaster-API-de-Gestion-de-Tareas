using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issues.Manager.Infrastructure.DBConfiguration;

public static class UserDataSeed
{

    public static void Configure(this ModelBuilder builder)
    {
        string id1 = new Guid().ToString();
        string id2 = new Guid().ToString();
        builder.Entity<IdentityUser>().HasData(new[]
        {
            new IdentityUser()
            {
                UserName = "Gperez18",
                Email = "GPerez18@gmail.com",
                Id = id1
            },
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
                FullName = "Geraldson Perez",
                Id = 1,
                IdentityId = id1,
            },
            new User()
            {
                FullName = "John Doe",
                Id = 2,
                IdentityId = id2,
            }
        });
    }
}