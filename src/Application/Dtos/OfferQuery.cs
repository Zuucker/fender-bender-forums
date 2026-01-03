using Domain.Models;

namespace Application.Dtos
{
    public class OfferQuery
    {
        public Offer Offer { get; set; } = null!;

        public int LikeCount { get; set; }
    }
}
