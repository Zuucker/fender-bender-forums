using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties can go here

        public string Guid { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = "/src/assets/vue.svg"; // default value for now
    }
}
