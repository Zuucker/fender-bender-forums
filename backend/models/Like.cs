using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public int AuthorId { get; set; }

        [Required]
        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }

        [Required]
        [ForeignKey("Comment")]
        public int CommentId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
