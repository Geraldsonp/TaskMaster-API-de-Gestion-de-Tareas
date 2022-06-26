using Issues.Manager.DataAccess.DBConfiguration;
using Microsoft.EntityFrameworkCore;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Issues.Manager.DataAccess;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUser>().HasOne<User>().WithOne(u => u.IdentityUser)
            .HasForeignKey<User>(u => u.IdentityId);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<User> Users { get; set; }
    
}