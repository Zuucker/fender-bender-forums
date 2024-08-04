using System.ComponentModel.DataAnnotations;

namespace backend.models
{
    public class AdditionalContent
    {
        [Key]
        public int AdditionalContentId { get; set; }

        [Required]
        [StringLength(100)]
        public int Type { get; set; }

        [Required]
        [StringLength(100)]
        public int Content { get; set; }

        [Required]
        [StringLength(100)]
        public int Path { get; set; }
    }
}
