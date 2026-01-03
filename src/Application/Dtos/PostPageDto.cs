using Application.Dtos.ModelDtos;

namespace Application.Dtos
{
    public class PostPageDto
    {
        public List<PostDto> Posts { get; set; } = [];

        public string? NextCursor { get; set; }
    }
}
