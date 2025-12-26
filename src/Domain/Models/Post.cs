using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [Required]
        public int SectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(100)]
        public int Content { get; set; }

        [Required]
        [StringLength(100)]
        public string Tags { get; set; } = string.Empty;

        [InverseProperty("Post")]
        public ICollection<Content> Contents { get; set; } = [];

        [ForeignKey("AuthorId")]
        [InverseProperty("Posts")]
        public ApplicationUser? User { get; set; }

        [ForeignKey("SectionId")]
        [InverseProperty("Posts")]
        public Section? Section { get; set; }

        [InverseProperty("Post")]
        public ICollection<Comment> Comments { get; set; } = [];

        [InverseProperty("Post")]
        public ICollection<Like> Likes { get; set; } = [];
    }
}
