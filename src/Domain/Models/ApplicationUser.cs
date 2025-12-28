using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties can go here

        public string Guid { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = "/src/assets/vue.svg"; // default value for now


        [InverseProperty("User")]
        public ICollection<Post> Posts { get; set; } = [];
        
        [InverseProperty("User")]
        public ICollection<Offer> Offers { get; set; } = [];

        [InverseProperty("User")]
        public ICollection<Like> Likes { get; set; } = [];

        [InverseProperty("User")]
        public ICollection<Comment> Comments { get; set; } = [];
    }
}
