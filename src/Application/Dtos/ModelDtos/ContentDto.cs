using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class ContentDto
    {
        public ContentDto(Content org)
        {
            Id = org.ContentId;
            PostId = org.PostId;
            OfferId = org.OfferId;
            Type = org.Type;
            TextContent = org.TextContent;
            Position = org.Position;
            GalleryElements = org.GalleryElements
                .Select(ge => new GalleryElementDto(ge))
                .ToList();
        }

        public ContentDto() { }

        public int Id { get; set; }

        public int? PostId { get; set; }

        public int? OfferId { get; set; }

        public int Type { get; set; }

        public string TextContent { get; set; } = string.Empty;

        public int Position { get; set; }

        public List<GalleryElementDto> GalleryElements { get; set; } = [];
    }
}
