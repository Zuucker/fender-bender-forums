using System.ComponentModel.DataAnnotations;

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
        public int ParentId { get; set; }
    }
}
