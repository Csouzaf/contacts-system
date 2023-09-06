using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.auth.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class UserRegisteredDbContext : DbContext
    {
        public UserRegisteredDbContext(DbContextOptions<UserRegisteredDbContext> options) : base(options)
        {

        }

        public DbSet<UserRegisteredModel> userRegisteredModel { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}