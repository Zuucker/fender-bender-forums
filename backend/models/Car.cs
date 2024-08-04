using System.ComponentModel.DataAnnotations;

namespace backend.models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [DataType(DataType.Date)]
        public DateTime Year { get; set; }

        [Required]
        [StringLength(100)]
        public string Generation { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }
    }
}
