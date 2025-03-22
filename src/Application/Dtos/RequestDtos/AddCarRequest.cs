using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class AddCarRequest
    {
        [Required(ErrorMessage = "Manufacturer is required")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; } = string.Empty;

        public DateTime Year { get; set; }

        public string Generation { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
