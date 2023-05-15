using Microsoft.EntityFrameworkCore;
namespace api.Models

{
    public class HomeDbContextModel : DbContext
    {
        public HomeDbContextModel(DbContextOptions<HomeDbContextModel> options) : base(options)
        {

        }

        public DbSet<HomeUser> homeUsers { get; set; }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}