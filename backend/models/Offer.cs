using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
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
        public string Color { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        [StringLength(100)]
        public string Tags { get; set; }

        [Required]
        public int AdditionalContentId { get; set; }

        public virtual Car Car { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}
