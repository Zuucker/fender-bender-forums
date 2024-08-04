using System.ComponentModel.DataAnnotations;

namespace backend.Data
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public float Altitude { get; set; }

        [Required]
        public float Latitude { get; set; }
    }
}
