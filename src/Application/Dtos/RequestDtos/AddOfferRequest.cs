using Application.Dtos.ModelDtos;

namespace Application.Dtos.RequestDtos
{
    public class AddOfferRequest
    {
        public float Price { get; set; }

        public int CarId { get; set; }

        public int CityId { get; set; }

        public string AuthorId { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public int Condition { get; set; }

        public int Fuel { get; set; }

        public string Color { get; set; } = string.Empty;

        public int Mileage { get; set; }

        public string Tags { get; set; } = string.Empty;

        public List<ContentDto> AdditionalContents { get; set; } = [];
    }
}
