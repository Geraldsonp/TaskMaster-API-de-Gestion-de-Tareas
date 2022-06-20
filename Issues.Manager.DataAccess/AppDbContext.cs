using Microsoft.EntityFrameworkCore;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    
    public DbSet<Issue> Issues { get; set; }
    public DbSet<User> Users { get; set; }
    
}