using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class CommentDto
    {
        public CommentDto() { }

        public CommentDto(Comment org, ApplicationUser user)
        {
            CommentId = org.CommentId;
            Content = org.Content;
            Upvotes = org.Upvotes;
            ParentId = org.ParentId;
            PostId = org.PostId;
            OfferId = org.OfferId;
            CreatedAt = org.CreatedAt;
            Points = org.Likes
                .Sum(l => l.Up ? 1 : -1);
            Author = new UserDto(org.User);
            SubComments = org.SubComments
                .Select(sc => new CommentDto(sc, sc.User!))
                .ToList();
            UpVoted = org.Likes
                .Any(l => l.AuthorId == user.Id && l.Up);
            DownVoted = org.Likes
                .Any(l => l.AuthorId == user.Id && !l.Up);
        }

        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int Upvotes { get; set; }

        [Required]
        public int? ParentId { get; set; }

        public int? PostId { get; set; }

        public int? OfferId { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool UpVoted { get; set; }

        public bool DownVoted { get; set; }

        public int Points { get; set; }

        public UserDto? Author { get; set; }

        public List<CommentDto> SubComments { get; set; } = [];
    }
}
