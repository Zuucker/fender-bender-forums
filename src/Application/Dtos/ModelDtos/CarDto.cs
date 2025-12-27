using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class CarDto
    {
        public CarDto() { }

        public CarDto(Car org)
        {
            CarId = org.CarId;
            Manufacturer = org.Manufacturer;
            Model = org.Model;
            Year = org.Year;
            Generation = org.Generation;
            Type = org.Type;
        }

        public int CarId { get; set; }

        public string Manufacturer { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public DateTime Year { get; set; }

        public string Generation { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
