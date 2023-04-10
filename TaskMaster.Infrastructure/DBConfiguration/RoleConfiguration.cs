using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskMaster.Infrastructure.DBConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
	public void Configure(EntityTypeBuilder<IdentityRole> builder)
	{
		builder.HasData(
			new IdentityRole
			{
				Name = "Project Manager",
				NormalizedName = "PROJECT MANAGER"
			},
			new IdentityRole
			{
				Name = "Developer",
				NormalizedName = "DEVELOPER"
			},
			new IdentityRole
			{
				Name = "Quality Assurance",
				NormalizedName = "Quality Assurance"
			},
			new IdentityRole
			{
				Name = "Administrator",
				NormalizedName = "ADMINISTRATOR"
			});

	}
}