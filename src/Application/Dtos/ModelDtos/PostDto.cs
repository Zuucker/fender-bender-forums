using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class PostDto
    {
        public PostDto(Post org)
        {
            AuthorId = org.AuthorId;
            SectionId = org.SectionId;
            Title = org.Title;
            CreationDate = org.CreationDate;
            Content = org.Content;
            Tags = org.Tags;
            Contents = org.Contents
                .Select(c => new ContentDto(c))
                .ToList();
            Author = new UserDto(org.User);
            Section = org.Section;
        }

        public PostDto() { }

        public int Id { get; set; }

        public string AuthorId { get; set; } = string.Empty;

        public int SectionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public int Content { get; set; }

        public string Tags { get; set; } = string.Empty;

        public UserDto? Author { get; set; }

        public Section? Section { get; set; }

        public virtual ICollection<ContentDto> Contents { get; set; } = [];
    }
}
