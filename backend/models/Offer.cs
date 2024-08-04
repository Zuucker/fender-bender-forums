using System.ComponentModel.DataAnnotations;

namespace backend.models
{
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }

        [Required]
        public float Price { get; set; }

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
    }
}
