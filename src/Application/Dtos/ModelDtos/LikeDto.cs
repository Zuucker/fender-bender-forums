using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class LikeDto
    {
        public LikeDto() { }

        public LikeDto(Like org)
        {
            Id = org.LikeId;
            AuthorId = org.AuthorId;
            OfferId = org.OfferId;
            PostId = org.PostId;
            CommentId = org.CommentId;
            UpVoted = org.Up;
            DownVoted = !org.Up;
        }

        public int Id { get; set; }

        public string AuthorId { get; set; } = string.Empty;

        public int? OfferId { get; set; }

        public int? PostId { get; set; }

        public int? CommentId { get; set; }

        public bool UpVoted { get; set; } 

        public bool DownVoted { get; set; } 
    }
}
