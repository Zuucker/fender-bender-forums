using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int Upvotes { get; set; }

        public string AuthorId { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public int? PostId { get; set; }

        public int? OfferId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("Comments")]
        public virtual ApplicationUser? User { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Comments")]
        public virtual Post? Post { get; set; }

        [ForeignKey("OfferId")]
        [InverseProperty("Comments")]
        public virtual Offer? Offer { get; set; }

        [InverseProperty("Comment")]
        public virtual ICollection<Like> Likes { get; set; } = [];

        [ForeignKey("ParentId")]
        [InverseProperty("SubComments")]
        public virtual Comment? ParentComment { get; set; }

        [InverseProperty("ParentComment")]
        public virtual ICollection<Comment> SubComments { get; set; } = [];

    }
}
