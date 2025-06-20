using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IPostService
    {
        public ServiceResult<Post> AddPost(AddPostRequest postRequest);

        public ServiceResult<IEnumerable<Post>> GetAllPosts();
    }
}
