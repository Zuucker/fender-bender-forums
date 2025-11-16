using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string AuthorId { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Section")]
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

        public virtual ICollection<Content> Contents { get; set; } = [];

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual Section Section { get; set; } = null!;
    }
}
