using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class GalleryElement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ContentId { get; set; }

        public int GalleryPosition { get; set; }

        [Required]
        public string Path { get; set; } = string.Empty;

        [ForeignKey("ContentId")]
        [JsonIgnore]
        public virtual Content? Content { get; set; }
    }
}
