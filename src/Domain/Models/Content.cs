using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Content
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [StringLength(400)]
        public string SubTitle { get; set; } = string.Empty;

        [Required]
        public int Position { get; set; }

        [JsonIgnore]
        public virtual Offer? Offer { get; set; }

        [JsonIgnore]
        public virtual Post? Post { get; set; }

        [InverseProperty("Content")]
        [JsonIgnore]
        public virtual ICollection<GalleryElement> GalleryElements { get; set; } = [];
    }
}
