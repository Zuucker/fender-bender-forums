using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Content
    {
        [Key]
        public int ContentId { get; set; }

        [ForeignKey("Post")]
        public int? PostId { get; set; }

        [ForeignKey("Offer")]
        public int? OfferId { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        [StringLength(1000)]
        public string TextContent { get; set; } = string.Empty;

        [Required]
        public int Position { get; set; }

        public int? GalleryPosition { get; set; }

        [Required]
        [StringLength(100)]
        public string Path { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual Offer? Offer { get; set; }

        [JsonIgnore]
        public virtual Post? Post { get; set; }
    }
}
