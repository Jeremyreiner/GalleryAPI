using Gallery.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gallery.DataBase.Infrastructure.MySql
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<User> Users{ get; set; } = null!;

        public DbSet<GalleryModel> GalleryItems{ get; set; } = null!;
    }
}