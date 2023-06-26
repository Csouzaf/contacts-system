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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<UsersAuth>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });
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