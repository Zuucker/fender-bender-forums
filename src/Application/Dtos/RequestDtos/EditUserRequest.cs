using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.RequestDtos
{
    public class EditUserRequest
    {
        [Required(ErrorMessage ="UserId is required")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email {  get; set; } = string.Empty;

        //TODO avatar
    }
}
