using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class InteractWithOfferRequest
    {
        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [Required]
        public int OfferId { get; set; }

        [Required]
        public bool Upvoted { get; set; }

        [Required]
        public bool DownVoted { get; set; }
    }
}

