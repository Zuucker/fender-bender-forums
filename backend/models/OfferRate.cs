using System.ComponentModel.DataAnnotations;

namespace backend.models
{
    public class OfferRate
    {
        [Key]
        public int OfferRateId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int OfferId { get; set; }

        [Required]
        public int Score { get; set; }
    }
}
