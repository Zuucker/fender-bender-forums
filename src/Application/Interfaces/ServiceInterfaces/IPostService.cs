using Application.Common;
using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IPostService
    {
        public ServiceResult<Post> AddPost(AddPostRequest postRequest);

        public ServiceResult<IEnumerable<Post>> GetAllPosts();

        public ServiceResult<IEnumerable<Post>> GetUsersPosts(Guid userId);

        public ServiceResult<Post> GetById(int postId);

        public ServiceResult<Comment> AddComment(AddCommentRequest commentRequest);

        public ServiceResult<Like> InteractWithPost(InteractWithPostRequest interactionRequest, ApplicationUser user);

        public ServiceResult<Like> InteractWithComment(InteractWithCommentRequest interactionRequest, ApplicationUser user);
    }
}
