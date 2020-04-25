using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyForum.Models;
using MyForum.Data;

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
        public DbSet<Post> Genders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gender>().HasData(
                new Gender[]
                {
                    new Gender {Id = 1, Name = "Female"},
                    new Gender {Id = 2, Name = "Male"}
                });
        }
    }
}
