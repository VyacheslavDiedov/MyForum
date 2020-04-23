using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyForum.Models;

namespace MyForum.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Topic> TypeTopics { get; set; }
        public DbSet<Post> Posts { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TypeTopic>().HasData(
            new TypeTopic[]
            {
                    new TypeTopic {Id = 1, Name = "General"},
                    new TypeTopic {Id = 2, Name = "Other"}
            });
    }
    }
}
