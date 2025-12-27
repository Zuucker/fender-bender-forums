using Application.Dtos.ModelDtos;
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

        public string Tags { get; set; } = string.Empty;

        public int? CarId { get; set; }

        [Required]
        public List<ContentDto> Contents { get; set; } = [];
    }
}
