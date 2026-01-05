using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<GalleryElement> GalleryElements { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Offer>()
                .HasIndex(o => o.Tags)
                .HasMethod("gin");

            modelBuilder.Entity<Post>()
                .HasIndex(o => o.Tags)
                .HasMethod("gin");
        }
    }
}
