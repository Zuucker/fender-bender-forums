using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure.Persistance.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<AdditionalContent> AdditionalContents { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<OfferRate> OfferRates { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Section> Sections { get; set; }


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<ApplicationUser>().HasKey(e => e.Id);
        //     modelBuilder.Entity<Car>().HasKey(e => e.CarId);
        //     modelBuilder.Entity<Offer>(entity =>
        //     {
        //         entity.HasKey(e => e.OfferId);
        //         entity.HasOne(e => e.Owner);
        //         entity.WithMany(b => b.Posts);
        //         entity.HasForeignKey(p => p.BlogId);
        //     });
        // }
    }
}
