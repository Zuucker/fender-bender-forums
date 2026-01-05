using Application.Dtos.ModelDtos;
using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Dtos.RequestDtos
{
    public class AddOfferRequest
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int CarId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [Required]
        public string Condition { get; set; } = string.Empty;

        public string? Fuel { get; set; }

        public string? Color { get; set; }

        public int? Mileage { get; set; }
        
        public string? VIN { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;

        public string? PartNumber { get; set; }

        public List<TagDto> Tags { get; set; } = [];

        [Required]
        public List<ContentDto> Contents { get; set; } = [];
    }
}
