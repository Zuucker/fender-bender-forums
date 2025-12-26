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

        public int? PostId { get; set; }

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
        [ForeignKey("OfferId")]
        [InverseProperty("Contents")]
        public virtual Offer? Offer { get; set; }

        [JsonIgnore]
        [ForeignKey("PostId")]
        [InverseProperty("Contents")]
        public virtual Post? Post { get; set; }

        [JsonIgnore]
        [InverseProperty("Content")]
        public virtual ICollection<GalleryElement> GalleryElements { get; set; } = [];
    }
}
