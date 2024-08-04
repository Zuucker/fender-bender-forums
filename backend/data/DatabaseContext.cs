using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<AdditionalContent> AdditionalContents { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Car> OfferRates { get; set; }
        public DbSet<OfferRate> Posts { get; set; }
        public DbSet<Section> Sections { get; set; }
    }
}
