using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public float Altitude { get; set; }

        [Required]
        public float Latitude { get; set; }

        public City() { }

        //public City(AddCityRequest dto)
        //{
        //    Name = dto.Name;
        //    Altitude = dto.Altitude;
        //    Latitude = dto.Latitude;
        //}
    }
}
