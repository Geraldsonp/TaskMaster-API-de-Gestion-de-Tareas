using System.Security.Claims;
using System.Security.Principal;
using Issues.Manager.Application.Services.HttpContextAccessor;
using Issues.Manager.Infrastructure.DBConfiguration;
using Microsoft.EntityFrameworkCore;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Issues.Manager.Infrastructure;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IHttpAccessor _httpAccessor;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpAccessor httpAccessor) : base(options)
    {
        _httpAccessor = httpAccessor;
    }
    
  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUser>().HasOne<User>().WithOne(u => u.IdentityUser)
            .HasForeignKey<User>(u => u.IdentityId);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.Entity<Issue>()
                .HasQueryFilter(i => i.UserId == _httpAccessor.GetCurrentIdentityId() );
    }
    
    
    
    public DbSet<Issue> Issues { get; set; }
    public DbSet<User> AppUsers { get; set; }
    
}