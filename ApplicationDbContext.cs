﻿using Microsoft.EntityFrameworkCore;
using NET6_WEB_API_TEMPLATE_JWT.Entities;
namespace NET6_WEB_API_TEMPLATE_JWT;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TI"); //SCHEMA CREATE
        base.OnModelCreating(modelBuilder);
    }

    //ENTITIES ON DB
    public DbSet<User> Users { get; set; }
    public DbSet<Rol> Roles { get; set; }

}