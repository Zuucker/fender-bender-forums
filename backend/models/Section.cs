using System.ComponentModel.DataAnnotations;

namespace backend.models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
