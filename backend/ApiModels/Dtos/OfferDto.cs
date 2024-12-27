using Models;

namespace Backend.ApiModels.Dtos
{
    public class OfferDto
    {
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

        public virtual Car Car { get; set; } = null!;

        public virtual UserDto Owner { get; set; } = null!;

        public virtual City City { get; set; } = null!;

        public virtual ICollection<AdditionalContent> AdditionalContents { get; set; } = [];


        public OfferDto(Offer original)
        {
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
            Owner = new UserDto(original.Owner);
            AdditionalContents = original.AdditionalContents;
        }
    }
}