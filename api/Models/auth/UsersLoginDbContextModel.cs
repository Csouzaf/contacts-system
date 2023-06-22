using Microsoft.EntityFrameworkCore;
namespace api.Models.auth

{
    public class UsersLoginDbContextModel : DbContext
    {
        public UsersLoginDbContextModel(DbContextOptions<UsersLoginDbContextModel> options) : base(options)
        {

        }

        public DbSet<UsersLogin> usersLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<UsersLogin>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}