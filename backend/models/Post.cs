using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public int AuthorId { get; set; }

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

        [Required]
        public int AdditionalContentId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual Section Section { get; set; } = null!;
    }
}
