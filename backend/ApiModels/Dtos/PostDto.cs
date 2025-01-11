using Backend.ApiModels.Dtos;

namespace Models
{
    public class PostDto
    {
        public PostDto() { }

        public PostDto(Post org)
        {
            AuthorId = org.AuthorId;
            SectionId = org.SectionId;
            Title = org.Title;
            CreationDate = org.CreationDate;
            Content = org.Content;
            Tags = org.Tags;
            AdditionalContents = org.AdditionalContents;
            Author = new UserDto(org.User);
        }

        public int PostId { get; set; }

        public string AuthorId { get; set; } = string.Empty;

        public int SectionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public int Content { get; set; }

        public string Tags { get; set; } = string.Empty;

        public int AdditionalContentId { get; set; }

        public UserDto Author { get; set; }

        public virtual ICollection<AdditionalContent> AdditionalContents { get; set; } = [];
    }
}
