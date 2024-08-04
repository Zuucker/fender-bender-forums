using System.ComponentModel.DataAnnotations;

namespace backend.models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ParentId { get; set; }
    }
}
