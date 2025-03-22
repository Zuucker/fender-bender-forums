using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
