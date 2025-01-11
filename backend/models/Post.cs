using Backend.ApiModels.Requests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Post
    {
        public Post(AddPostRequest request)
        {
            AuthorId = request.AuthorId;
            SectionId = request.SectionId;
            Title = request.Title;
            CreationDate = request.CreationDate;
            Content = request.Content;
            Tags = request.Tags;
            AdditionalContents = request.AdditionalContents
                .Select(ac => new AdditionalContent(ac))
                .ToList();
        }

        public Post() { }

        [Key]
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

        public virtual ICollection<AdditionalContent> AdditionalContents { get; set; } = [];

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual Section Section { get; set; } = null!;
    }
}
