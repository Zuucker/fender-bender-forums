using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Models;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository repository)
        {
            _postRepository = repository;
        }

        public ServiceResult<Post> AddPost(AddPostRequest postRequest)
        {
            Post newPost = PostFactory.Create(postRequest);

            _postRepository.Add(newPost);

            return ServiceResult<Post>.Success(newPost);
        }

        public ServiceResult<IEnumerable<Post>> GetAllPosts()
        {
            var posts = _postRepository
                .GetAll();

            return ServiceResult<IEnumerable<Post>>.Success(posts);
        }
    }
}
