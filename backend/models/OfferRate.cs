using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
{
    public class OfferRate
    {
        [Key]
        public int OfferRateId { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        [Required]
        public int Score { get; set; }

        public virtual Offer Owner { get; set; } = null!;
    }
}
