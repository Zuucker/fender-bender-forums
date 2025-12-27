using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }

        [Required]
        [StringLength(150)]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Model { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Year { get; set; }

        [Required]
        [StringLength(150)]
        public string Generation { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Type { get; set; } = string.Empty;

        [InverseProperty("Car")]
        public ICollection<Post> Posts { get; set; } = [];
    }
}
