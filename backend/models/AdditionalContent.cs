using Backend.ApiModels.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class AdditionalContent
    {
        [Key]
        public int AdditionalContentId { get; set; }

        [ForeignKey("Post")]
        public int? PostId { get; set; }

        [ForeignKey("Offer")]
        public int? OfferId { get; set; }

        [Required]
        [StringLength(100)]
        public int Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Content { get; set; }

        [Required]
        [StringLength(100)]
        public string Path { get; set; }

        [JsonIgnore]
        public virtual Offer Offer { get; set; } = null!;

        [JsonIgnore]
        public virtual Post Post { get; set; } = null!;


#pragma warning disable CS8618 // Remove warning rule
        //constructor for EF
        public AdditionalContent() { }
#pragma warning restore CS8618 // Restore warning rule


        public AdditionalContent(AdditionalContentDto dto)
        {
            PostId = dto.PostId;
            OfferId = dto.OfferId;
            Type = dto.Type;
            Content = dto.Content;
            Path = dto.Path;
        }
    }
}
