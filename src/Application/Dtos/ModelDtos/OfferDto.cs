using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class OfferDto
    {
        public OfferDto(Offer original)
        {
            OfferId = original.OfferId;
            Price = original.Price;
            CarId = original.CarId;
            CityId = original.CityId;
            AuthorId = original.AuthorId;
            Date = original.Date;
            Condition = original.Condition;
            Fuel = original.Fuel;
            Color = original.Color;
            Mileage = original.Mileage;
            Tags = original.Tags;
            Car = original.Car;
            City = original.City;
            Rating = original.Rating;
            RatingCount = original.RatingCount;
            Owner = new UserDto(original.Owner);
            AdditionalContents = original.AdditionalContents;
            Ratings = original.Ratings
                .Select(r => new OfferRateDto(r))
                .OrderBy(r => r.CreatedAt)
                .ToList();
        }

        public int OfferId { get; set; }

        public float Price { get; set; }

        public int CarId { get; set; }

        public int CityId { get; set; }

        public string AuthorId { get; set; }

        public DateTime Date { get; set; }

        public int Condition { get; set; }

        public int Fuel { get; set; }

        public string Color { get; set; } = string.Empty;

        public int Mileage { get; set; }

        public string Tags { get; set; } = string.Empty;

        public float Rating { get; set; }

        public int RatingCount { get; set; }

        public virtual Car Car { get; set; } = null!;

        public virtual UserDto Owner { get; set; } = null!;

        public virtual City City { get; set; } = null!;

        public virtual ICollection<Content> AdditionalContents { get; set; } = [];

        public virtual ICollection<OfferRateDto> Ratings { get; set; } = [];
    }
}