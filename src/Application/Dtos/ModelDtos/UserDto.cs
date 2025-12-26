using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Id { get; set; } = string.Empty;

        public string Avatar { get; set; } = string.Empty;

        public float Rating { get; set; } = 0;

        public string Password { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;

        //add more fields if necesarry

        public UserDto() { }

        public UserDto(ApplicationUser? user)
        {
            UserName = user?.UserName ?? string.Empty;
            Email = user?.Email ?? string.Empty;
            Id = user?.Id ?? string.Empty;
            Avatar = "/src/assets/vue.svg" ?? user.AvatarUrl;
        }
    }
}
