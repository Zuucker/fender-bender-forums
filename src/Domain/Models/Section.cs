using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        public int? ParentSectionId { get; set; }

        [ForeignKey("ParentSectionId")]
        [InverseProperty("SubSections")]
        [JsonIgnore]
        public Section? ParentSection { get; set; }

        [InverseProperty("ParentSection")]
        public List<Section> SubSections { get; set; } = [];
    }
}
