using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class AddCommentRequest
    {
        [Required]
        public string Content { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public int? PostId { get; set; }

        public int? OfferId { get; set; }

        [Required]
        public string AuthorId { get; set; } = string.Empty;
    }
}
