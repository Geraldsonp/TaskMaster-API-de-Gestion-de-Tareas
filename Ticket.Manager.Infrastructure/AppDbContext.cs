using System.Security.Claims;
using Issues.Manager.Infrastructure.DBConfiguration;
using Microsoft.EntityFrameworkCore;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Issues.Manager.Infrastructure;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> AppUsers { get; set; }
  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUser>().HasOne<User>().WithOne(u => u.IdentityUser)
            .HasForeignKey<User>(u => u.IdentityId);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.Entity<Ticket>()
                .HasQueryFilter(i => i.UserId == GetUserId() );
    }

    private int GetUserId()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return AppUsers.FirstOrDefault(u => u.IdentityId == userIdClaim).Id;
        }
        else
        {
            return 0;
        }
    }
    
    
    
    
    
}