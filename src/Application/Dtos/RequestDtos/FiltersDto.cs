using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class FiltersDto
    {
        public string? Title { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public int? CarId { get; set; }

        public int? CityId { get; set; }

        public string? AuthorId { get; set; }

        public string? Condition { get; set; }

        public string? Fuel { get; set; }

        public string? Color { get; set; }

        public int? Mileage { get; set; }

        public string? Tags { get; set; }

        public string? PartNumber { get; set; }

        public string? Type { get; set; }

        public int? SectionId { get; set; }

        public DateTime? CreationDate { get; set; }

        [Required]
        public string SortBy { get; set; } = string.Empty;

        [Required]
        public string OrderBy { get; set; } = string.Empty;
    }
}
