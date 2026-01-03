using Domain.Models;

namespace Application.Dtos
{
    public class PostQuery
    {
        public Post Post { get; set; } = null!;

        public int LikeCount { get; set; }
    }
}
