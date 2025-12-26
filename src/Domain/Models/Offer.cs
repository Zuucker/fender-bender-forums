using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [ForeignKey("Car")]
        public int CarId { get; set; }

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public string AuthorId { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int Condition { get; set; }

        [Required]
        public int Fuel { get; set; }

        [Required]
        [StringLength(100)]
        public string Color { get; set; } = string.Empty;

        [Required]
        public int Mileage { get; set; }

        [Required]
        [StringLength(100)]
        public string Tags { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float Rating { get; private set; }//db should manage it

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int RatingCount { get; private set; }//db should manage it

        [Required]
        public string Title { get; set; } = string.Empty;

        public virtual Car? Car { get; set; }

        public virtual ApplicationUser? Owner { get; set; }

        public virtual City? City { get; set; }

        [InverseProperty("Offer")]
        public virtual ICollection<Content> Contents { get; set; } = [];

        public virtual ICollection<OfferRate> Ratings { get; set; } = [];

        [InverseProperty("Offer")]
        public virtual ICollection<Comment> Comments { get; set; } = [];

        [InverseProperty("Offer")]
        public virtual ICollection<Like> Likes { get; set; } = [];
    }
}
