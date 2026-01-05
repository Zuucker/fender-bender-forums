using Application.Dtos.ModelDtos;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class AddPostRequest
    {
        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [Required]
        public int SectionId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public int Content { get; set; }

        public List<TagDto> Tags { get; set; } = [];

        public int? CarId { get; set; }

        [Required]
        public List<ContentDto> Contents { get; set; } = [];
    }
}
