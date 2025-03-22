namespace Application.Dtos.ModelDtos
{
    public class ContentDto
    {
        public int? PostId { get; set; }

        public int? OfferId { get; set; }

        public int Type { get; set; }

        public string TextContent { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;

        public int Position { get; set; }

        public int? GalleryPosition { get; set; }
    }
}
