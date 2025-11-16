using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Models;
using System.Transactions;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IFileStorage _fileStorage;

        public PostService(IPostRepository repository, IFileStorage fileStorage)
        {
            _postRepository = repository;
            _fileStorage = fileStorage;
        }

        public ServiceResult<Post> AddPost(AddPostRequest postRequest)
        {
            using var scope = new TransactionScope();

            try
            {
                foreach (GalleryElementDto ge in postRequest.Contents.SelectMany(c => c.GalleryElements))
                {
                    string path = _fileStorage
                        .WriteImageFile(ge.Base64Data.Split(",")[1]);

                    ge.Path = path;
                };

                Post newPost = PostFactory.Create(postRequest);

                _postRepository.Add(newPost);

                scope.Complete();

                return ServiceResult<Post>.Success(newPost);

            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }

        public ServiceResult<IEnumerable<Post>> GetAllPosts()
        {
            var posts = _postRepository
                .GetAll()
                .OrderByDescending(p => p.CreationDate)
                .Take(10);

            return ServiceResult<IEnumerable<Post>>.Success(posts);
        }

        public ServiceResult<IEnumerable<Post>> GetUsersPosts(Guid userId)
        {
            var posts = _postRepository
                .GetUsersPosts(userId);

            return ServiceResult<IEnumerable<Post>>.Success(posts);
        }
    }
}
