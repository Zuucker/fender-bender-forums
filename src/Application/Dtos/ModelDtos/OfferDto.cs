using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class OfferDto
    {
        public OfferDto(Offer org, ApplicationUser user)
        {
            OfferId = org.OfferId;
            Title = org.Title;
            Price = org.Price;
            CarId = org.CarId;
            CityId = org.CityId;
            AuthorId = org.AuthorId;
            Date = org.Date;
            Condition = org.Condition;
            Fuel = org.Fuel;
            Color = org.Color;
            Mileage = org.Mileage;
            Tags = org.Tags;
            Car = org.Car != null
                ? new CarDto(org.Car)
                : null;
            City = org.City != null
                ? new CityDto(org.City)
                : null;
            Points = org.Likes
                .Sum(l => l.Up ? 1 : -1);
            VIN = org.VIN;
            PartNumber = org.PartNumber;
            Type = org.Type;
            Author = new UserDto(org.User);
            Contents = org.Contents
                .Select(c => new ContentDto(c))
                .ToList();
            Comments = org.Comments
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentDto(c, user))
                .ToList();
            UpVoted = org.Likes
                .Any(l => l.AuthorId == user.Id && l.Up);
            DownVoted = org.Likes
                .Any(l => l.AuthorId == user.Id && !l.Up);
        }

        public int OfferId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int CarId { get; set; }

        public int CityId { get; set; }

        public string AuthorId { get; set; }

        public DateTime Date { get; set; }

        public string Condition { get; set; }

        public string? Fuel { get; set; }

        public string? Color { get; set; }

        public int? Mileage { get; set; }

        public string Tags { get; set; } = string.Empty;

        public float Points { get; set; }

        public string? VIN { get; set; }

        public string? PartNumber { get; set; }

        public string Type { get; set; } = string.Empty;

        public bool UpVoted { get; set; }

        public bool DownVoted { get; set; }

        public CarDto? Car { get; set; }

        public UserDto? Author { get; set; }

        public CityDto? City { get; set; }

        public ICollection<ContentDto> Contents { get; set; } = [];

        public ICollection<CommentDto> Comments { get; set; } = [];
    }
}