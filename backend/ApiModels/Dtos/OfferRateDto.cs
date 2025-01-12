using Models;

namespace Backend.ApiModels.Dtos
{
    public class OfferRateDto
    {
        public OfferRateDto(OfferRate org)
        {
            OfferRateId = org.OfferRateId;
            UserId = org.UserId;
            OfferId = org.OfferId;
            Rating = org.Rating;
            Comment = org.Comment;
            CreatedAt = org.CreatedAt;
            Owner = new(org.Owner);
        }
        public OfferRateDto() { }

        public int OfferRateId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int OfferId { get; set; }

        public float Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public UserDto Owner { get; set; } = null!;
    }
}
