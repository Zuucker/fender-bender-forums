using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Content { get; set; }

        [Required]
        public int Upvotes { get; set; }

        [Required]
        public int? ParentId { get; set; }

        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }

        [Required]
        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        public virtual Post Post { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
