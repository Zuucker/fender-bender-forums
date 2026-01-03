namespace Application.Dtos.RequestDtos
{
    public class OfferCursorDto
    {
        public DateTime CreatedAt { get; set; }

        public int LikeCount { get; set; }

        public int OfferId { get; set; }

        public int PostId { get; set; }

        public int Take { get; set; } = 20;
    }
}
