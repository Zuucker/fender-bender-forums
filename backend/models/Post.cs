using System.ComponentModel.DataAnnotations;

namespace backend.models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(100)]
        public int Content { get; set; }

        [Required]
        [StringLength(100)]
        public string Tags { get; set; }

        [Required]
        public int AdditionalContentId { get; set; }
    }
}
