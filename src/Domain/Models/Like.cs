using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }

        [Required]
        public string AuthorId { get; set; } = string.Empty;

        public int? OfferId { get; set; }

        public int? PostId { get; set; }

        public int? CommentId { get; set; }

        [Required]
        public bool Up { get; set; } // Up or Down - voted

        [ForeignKey("AuthorId")]
        [InverseProperty("Likes")]
        public virtual ApplicationUser? User { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Likes")]
        public virtual Post? Post { get; set; }

        [ForeignKey("OfferId")]
        [InverseProperty("Likes")]
        public virtual Offer? Offer { get; set; }

        [ForeignKey("CommentId")]
        [InverseProperty("Likes")]
        public virtual Comment? Comment { get; set; }
    }
}
