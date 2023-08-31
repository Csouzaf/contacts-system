using api.auth.Model;
using api.Models.auth.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Models.auth.Data

{
    public class UsersAuthDBContext : DbContext
    {
        public UsersAuthDBContext(DbContextOptions<UsersAuthDBContext> options) : base(options)
        {

        }

        public DbSet<UsersAuth> usersAuth { get; set; }
        public DbSet<AuthUserEmail> authUserEmails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //NOTE - UsersAuth provides unique email
            modelBuilder.Entity<UsersAuth>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });
            
            //NOTE - How UsersAuth is the main relantionship (father), it's came first instead authUserEmail
            modelBuilder.Entity<UsersAuth>()
                .HasOne(e => e.authUserEmail)
                .WithOne(e => e.UsersAuth)
                .HasForeignKey<AuthUserEmail>(e => e.UserAuthId);
            
            modelBuilder.Entity<UsersAuth>()
                .HasMany(n => n.contactsModel)
                .WithOne(n => n.usersAuth)
                .HasForeignKey(n => n.usersAuthId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        internal UsersAuth FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

    }
}