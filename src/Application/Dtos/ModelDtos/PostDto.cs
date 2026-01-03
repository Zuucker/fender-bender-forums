using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class PostDto
    {
        public PostDto(Post org, ApplicationUser? user)
        {
            Id = org.Id;
            AuthorId = org.AuthorId;
            SectionId = org.SectionId;
            Title = org.Title;
            CreationDate = org.CreationDate;
            Content = org.Content;
            Tags = org.Tags;
            Points = org.Likes
                .Sum(l => l.Up ? 1 : -1);
            Contents = org.Contents
                .Select(c => new ContentDto(c))
                .ToList();
            Author = new UserDto(org.User);
            Section = org.Section;
            Car = org.Car != null
                ? new CarDto(org.Car)
                : null;
            Comments = org.Comments
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentDto(c, user))
                .ToList();
            UpVoted = user != null
                && org.Likes
                .Any(l => l.AuthorId == user.Id && l.Up);
            DownVoted = user != null
                && org.Likes
                .Any(l => l.AuthorId == user.Id && !l.Up);
        }

        public PostDto() { }

        public int Id { get; set; }

        public string AuthorId { get; set; } = string.Empty;

        public int SectionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; }

        public int Content { get; set; }

        public string Tags { get; set; } = string.Empty;

        public bool UpVoted { get; set; }

        public bool DownVoted { get; set; }

        public int Points { get; set; }

        public UserDto? Author { get; set; }

        public Section? Section { get; set; }

        public CarDto? Car { get; set; }

        public ICollection<ContentDto> Contents { get; set; } = [];

        public ICollection<CommentDto> Comments { get; set; } = [];
    }
}
