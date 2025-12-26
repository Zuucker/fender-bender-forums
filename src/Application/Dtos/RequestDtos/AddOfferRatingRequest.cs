namespace Application.Dtos.RequestDtos
{
    public class AddOfferRatingRequest
    {
        public string UserId { get; set; } = string.Empty;

        public int OfferId { get; set; }

        public float Rating { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
