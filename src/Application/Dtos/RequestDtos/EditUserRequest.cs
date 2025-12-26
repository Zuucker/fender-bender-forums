using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class EditUserRequest
    {
        [Required(ErrorMessage ="UserId is required")]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Avatar is required")]
        public string Avatar { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        
        public string NewPassword { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
