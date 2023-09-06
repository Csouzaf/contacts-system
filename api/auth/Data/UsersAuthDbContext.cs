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
        public DbSet<UserRegisteredModel> userRegisteredModel { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //NOTE - UsersAuth provides unique email
            modelBuilder.Entity<UsersAuth>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });
            
            //NOTE - As UsersAuth is the main relantionship (father), it's came first instead authUserEmail
            modelBuilder.Entity<UsersAuth>()
                .HasOne(e => e.authUserEmail)
                .WithOne(e => e.UsersAuth)
                .HasForeignKey<AuthUserEmail>(e => e.UserAuthId);
            
            //NOTE - Relantionship 1 to 1 between UsersAuth plus UserRegisteredModel
            modelBuilder.Entity<UsersAuth>()
                .HasOne(n => n.userRegisteredModel)
                .WithOne(n => n.usersAuth)
                .HasForeignKey<UserRegisteredModel>(n => n.usersAuthenticatedId);


           //NOTE - Relantionship 1 to N between UserRegisteredModel plus contactsModel
            modelBuilder.Entity<UserRegisteredModel>()
                .HasMany(u => u.colletctionContactsModels)
                .WithOne(u => u.userRegisteredModel)
                .HasForeignKey(u => u.userRegisteredId);
                
            base.OnModelCreating(modelBuilder);
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