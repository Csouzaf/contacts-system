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

            
           //NOTE - Relantionship 1 to N between UserRegisteredModel plus contactsModel
            modelBuilder.Entity<UserRegisteredModel>()
                .HasMany(u => u.colletctionContactsModels)
                .WithOne(u => u.userRegisteredModel)
                .HasForeignKey(u => u.userRegisteredId)
                .OnDelete(DeleteBehavior.NoAction);           
            
            //NOTE - As UsersAuth is the main relantionship (father), it's came first instead authUserEmail
            modelBuilder.Entity<UsersAuth>()
                .HasOne(e => e.authUserEmail)
                .WithOne(e => e.UsersAuth)
                .HasForeignKey<AuthUserEmail>(e => e.userAuthId);
            
            //NOTE - Relantionship 1 to 0 between UsersAuth plus UserRegisteredModel
            //NOTE - Gotta be because when we create the user in signup router, we will need create besides authUserEmail the UserRegisteredModel too
            //NOTE - But the purpose is get usersAuth id in UserRegisteredModel and not opposite
            modelBuilder.Entity<UserRegisteredModel>()
                .HasOne(n => n.usersAuth)
                .WithOne()
                .HasForeignKey<UserRegisteredModel>(n => n.usersAuthenticatedId);
            
          //TODO - Resolver Relacionamento aqui

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