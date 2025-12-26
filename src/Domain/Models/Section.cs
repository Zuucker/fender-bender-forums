using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        public int? ParentSectionId { get; set; }

        [JsonIgnore]
        [ForeignKey("ParentSectionId")]
        [InverseProperty("SubSections")]
        public Section? ParentSection { get; set; }

        [InverseProperty("ParentSection")]
        public ICollection<Section> SubSections { get; set; } = [];

        [JsonIgnore]
        [InverseProperty("Section")]
        public ICollection<Post> Posts { get; set; } = [];
    }
}
