using Backend.ApiModels.Requests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class OfferRate
    {
        public OfferRate(AddOfferRatingRequest dto)
        {
            UserId = dto.UserId;
            OfferId = dto.OfferId;
            Rating = dto.Rating;
            Comment = dto.Comment;
            CreatedAt = DateTime.UtcNow;
        }
        public OfferRate() { }

        [Key]
        public int OfferRateId { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        [Required]
        public float Rating { get; set; }

        [StringLength(500)]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ApplicationUser Owner { get; set; } = null!;

        public virtual Offer Offer { get; set; } = null!;
    }
}
