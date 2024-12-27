using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class AdditionalContent
    {
        [Key]
        public int AdditionalContentId { get; set; }

        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }

        [Required]
        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        [Required]
        [StringLength(100)]
        public int Type { get; set; }

        [Required]
        [StringLength(100)]
        public int Content { get; set; }

        [Required]
        [StringLength(100)]
        public int Path { get; set; }

        public virtual Offer Offer { get; set; } = null!;

        public virtual Post Post { get; set; } = null!;
    }
}
