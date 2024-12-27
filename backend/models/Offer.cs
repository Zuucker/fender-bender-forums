using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Offer
    {
        [Key]
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
        [ForeignKey("AplicationUser")]
        public int AuthorId { get; set; }

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

        public virtual Car Car { get; set; } = null!;

        public virtual ApplicationUser Owner { get; set; } = null!;

        public virtual City City { get; set; } = null!;

        public virtual ICollection<AdditionalContent> AdditionalContents { get; set; } = [];
    }
}
