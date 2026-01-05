using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Index(nameof(Tags), Name = "IX_Posts_Tags", IsUnique = false)]
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

        [Column(TypeName = "jsonb")]
        public List<TagDto> Tags { get; set; } = [];
        
        public int? CarId { get; set; }

        [InverseProperty("Post")]
        public ICollection<Content> Contents { get; set; } = [];

        [ForeignKey("AuthorId")]
        [InverseProperty("Posts")]
        public ApplicationUser? User { get; set; }

        [ForeignKey("SectionId")]
        [InverseProperty("Posts")]
        public Section? Section { get; set; }
        
        [ForeignKey("CarId")]
        [InverseProperty("Posts")]
        public Car? Car { get; set; }

        [InverseProperty("Post")]
        public ICollection<Comment> Comments { get; set; } = [];

        [InverseProperty("Post")]
        public ICollection<Like> Likes { get; set; } = [];
    }
}
