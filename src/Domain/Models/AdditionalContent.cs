using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
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
        public string Content { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Path { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual Offer Offer { get; set; } = null!;

        [JsonIgnore]
        public virtual Post Post { get; set; } = null!;
    }
}
