using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Index(nameof(Tags), Name = "IX_Offers_Tags", IsUnique = false)]
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Condition { get; set; } = string.Empty;

        public string? Fuel { get; set; }

        [StringLength(100)]
        public string? Color { get; set; }

        public int? Mileage { get; set; }

        public string? VIN { get; set; }

        public string? PartNumber { get; set; }

        public string Type { get; set; } = string.Empty;

        [Column(TypeName = "jsonb")]
        public List<TagDto> Tags { get; set; } = [];

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float Rating { get; private set; }//db should manage it

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int RatingCount { get; private set; }//db should manage it

        [InverseProperty("Offer")]
        public ICollection<Content> Contents { get; set; } = [];

        [ForeignKey("AuthorId")]
        [InverseProperty("Offers")]
        public ApplicationUser? User { get; set; }
        
        [ForeignKey("CityId")]
        [InverseProperty("Offers")]
        public City? City { get; set; }

        [ForeignKey("CarId")]
        [InverseProperty("Offers")]
        public Car? Car { get; set; }

        [InverseProperty("Offer")]
        public ICollection<Comment> Comments { get; set; } = [];

        [InverseProperty("Offer")]
        public ICollection<Like> Likes { get; set; } = [];
    }
}
