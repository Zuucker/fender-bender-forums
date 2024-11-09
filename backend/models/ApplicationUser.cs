using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties can go here

        public string Guid { get; set; } = string.Empty;
    }
}
