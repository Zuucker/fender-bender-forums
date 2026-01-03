using Application.Dtos.ModelDtos;

namespace Application.Dtos
{
    public class OffersPageDto
    {
        public List<OfferDto> Offers { get; set; } = [];

        public string? NextCursor { get; set; }
    }
}
