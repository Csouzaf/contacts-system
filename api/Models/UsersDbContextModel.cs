using Microsoft.EntityFrameworkCore;
namespace api.Models

{
    public class UsersDbContextModel : DbContext
    {
        public UsersDbContextModel(DbContextOptions<UsersDbContextModel> options) : base(options)
        {

        }

        public DbSet<ContactsModel> contactsModel { get; set; }

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