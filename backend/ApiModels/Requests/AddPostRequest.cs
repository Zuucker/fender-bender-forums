using Backend.ApiModels.Dtos;

namespace Backend.ApiModels.Requests
{
    public class AddPostRequest
    {
        public string AuthorId { get; set; } = string.Empty;

        public int SectionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public int Content { get; set; }

        public string Tags { get; set; } = string.Empty;

        public List<AdditionalContentDto> AdditionalContents { get; set; } = [];
    }
}
