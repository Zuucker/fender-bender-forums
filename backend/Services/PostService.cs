using Backend.ApiModels.Dtos;
using Backend.ApiModels.Requests;
using Data;
using Models;

namespace Backend.Services
{
    public class PostService
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
            var posts = _repository
                .GetAll<Post>()
                .Select(p => new PostDto(p))
                .ToList();

            return posts;
        }
    }
}
