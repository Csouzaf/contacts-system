using Microsoft.EntityFrameworkCore;
namespace api.Models

{
    public class UsersDbContextModel : DbContext
    {
        public UsersDbContextModel(DbContextOptions<UsersDbContextModel> options) : base(options)
        {

        }

        public DbSet<UsersModel> usersModels { get; set; }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }
}