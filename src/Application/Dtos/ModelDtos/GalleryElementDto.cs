using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.ModelDtos
{
    public class GalleryElementDto
    {
        public GalleryElementDto(GalleryElement org)
        {
            Id = org.Id;
            ContentId = org.ContentId;
            GalleryPosition = org.GalleryPosition;
            Path = org.Path;
        }

        public GalleryElementDto() { }

        public int Id { get; set; }

        [Required]
        public int ContentId { get; set; }

        [Required]
        public int GalleryPosition { get; set; }

        public string Path { get; set; } = string.Empty;

        [Required]
        public string Base64Data { get; set; } = string.Empty;
    }
}
