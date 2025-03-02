using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Car
    {
        [Key]
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

        public Car() { }

        //public Car(AddCarRequest dto)
        //{
        //    Manufacturer = dto.Manufacturer;
        //    Model = dto.Model;
        //    Year = dto.Year;
        //    Generation = dto.Generation;
        //    Type = dto.Type;
        //}
    }
}
