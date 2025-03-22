using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class AddCityRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        public float Altitude { get; set; }

        public float Latitude { get; set; }
    }
}

