using Models;

namespace Backend.ApiModels.Dtos
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        //add more fields if necesarry


        public UserDto(ApplicationUser user)
        {
            Username = user.UserName!;
            Email = user.Email!;
            Id = user.Id!;
        }
    }
}
