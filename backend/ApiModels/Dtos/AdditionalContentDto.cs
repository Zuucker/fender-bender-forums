namespace Backend.ApiModels.Dtos
{
    public class AdditionalContentDto
    {
        public int? PostId { get; set; }

        public int? OfferId { get; set; }

        public int Type { get; set; }

        public string Content { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;
    }
}
