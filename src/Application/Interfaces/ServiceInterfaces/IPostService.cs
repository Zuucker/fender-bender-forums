using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IPostService
    {
        Post AddPost(AddPostRequest postRequest);

        ICollection<PostDto> GetAllPosts();
    }
}
