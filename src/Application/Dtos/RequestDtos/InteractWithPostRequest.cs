using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class InteractWithPostRequest
    {
        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [Required]
        public int PostId { get; set; }

        [Required]
        public bool Upvoted { get; set; }

        [Required]
        public bool DownVoted { get; set; }
    }
}
