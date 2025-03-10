using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Domain.Models;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly DataRepository _repository;

        public PostService(DataRepository repository)
        {
            _repository = repository;
        }

        public Post AddPost(AddPostRequest postRequest)
        {
            Post newPost = new(postRequest);

            newPost.AdditionalContents = postRequest.AdditionalContents
                .Select(ac => new AdditionalContent(ac))
                .ToList();


            _repository.Add(newPost);

            return newPost;
        }

        public ICollection<PostDto> GetAllPosts()
        {
            return _repository
                .GetAll<Post>()
                .Select(p => new PostDto(p))
                .ToList();
        }
    }
}
