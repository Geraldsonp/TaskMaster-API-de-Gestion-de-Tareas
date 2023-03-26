﻿using Issues.Manager.Application.Contracts;
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
    private readonly IUserIdProvider _userIdProvider;


    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor, IUserIdProvider userIdProvider) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _userIdProvider = userIdProvider;
    }
    
    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<Comment> Comments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.SeedDb();
    }

}