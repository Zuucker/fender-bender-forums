using Domain.Models;

namespace Application.Dtos.ModelDtos
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Id { get; set; } = string.Empty;

        public string Avatar {  get; set; } = string.Empty;

        //add more fields if necesarry


        public UserDto(ApplicationUser user)
        {
            UserName = user.UserName ?? string.Empty;
            Email = user.Email!;
            Id = user.Id!;
            Avatar = user.AvatarUrl;
        }
    }
}
