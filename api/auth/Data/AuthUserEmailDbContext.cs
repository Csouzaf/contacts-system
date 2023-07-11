
using api.auth.Model;
using api.Models.auth.Model;
using Microsoft.EntityFrameworkCore;

namespace api.auth.Data
{
    public class AuthUserEmailDbContext : DbContext
    {
        public AuthUserEmailDbContext(DbContextOptions<AuthUserEmailDbContext> options) : base(options)
        {

        }

        public DbSet<AuthUserEmail> authUserEmails { get; set; }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<AuthUserEmail>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // modelBuilder.Entity<AuthUserEmail>()
            // .HasOne(e =>e.UsersAuth)
            // .WithOne(e => e.authUserEmail)
            // .HasForeignKey<AuthUserEmail>(e=> e.UserAuthId);
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
          internal AuthUserEmail FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}