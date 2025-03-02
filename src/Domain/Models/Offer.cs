using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Offer
    {
#pragma warning disable CS8618 // Remove warning rule
        //constructor for EF
        public Offer() { }
#pragma warning restore CS8618 // Restore warning rule


        //public Offer(AddOfferRequest dto)
        //{
        //    Price = dto.Price;
        //    CarId = dto.CarId;
        //    CityId = dto.CityId;
        //    AuthorId = dto.AuthorId;
        //    Date = dto.Date;
        //    Condition = dto.Condition;
        //    Fuel = dto.Fuel;
        //    Color = dto.Color;
        //    Mileage = dto.Mileage;
        //    Tags = dto.Tags;
        //}

        [Key]
        public int OfferId { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [ForeignKey("Car")]
        public int CarId { get; set; }

        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        [Required]
        [ForeignKey("Owner")]
        public string AuthorId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int Condition { get; set; }

        [Required]
        public int Fuel { get; set; }

        [Required]
        [StringLength(100)]
        public string Color { get; set; } = string.Empty;

        [Required]
        public int Mileage { get; set; }

        [Required]
        [StringLength(100)]
        public string Tags { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public float Rating { get; private set; }//db should manage it

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int RatingCount { get; private set; }//db should manage it

        public virtual Car Car { get; set; } = null!;

        public virtual ApplicationUser Owner { get; set; } = null!;

        public virtual City City { get; set; } = null!;

        public virtual ICollection<AdditionalContent> AdditionalContents { get; set; } = [];

        public virtual ICollection<OfferRate> Ratings { get; set; } = [];
    }
}
