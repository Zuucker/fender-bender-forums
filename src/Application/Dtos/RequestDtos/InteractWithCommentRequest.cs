using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class InteractWithCommentRequest
    {
        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [Required]
        public int CommentId { get; set; }

        [Required]
        public bool Upvoted { get; set; }

        [Required]
        public bool DownVoted { get; set; }
    }
}

