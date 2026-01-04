using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;

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


            IEnumerable<Like> likes = user?.Posts.SelectMany(p => p.Likes)
                .Concat(user?.Offers.SelectMany(o => o.Likes) ?? []) ?? [];

            Rating = likes.Any()
                ? (float)(likes.Count(l => l.Up) * 5.0 / likes.Count())
                : 3f;
        }
    }
}
