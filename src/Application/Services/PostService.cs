using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Domain.Models;
using System.Transactions;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;
        private readonly IFileStorage _fileStorage;

        public PostService(
            ICommentRepository commentRepository,
            ILikeRepository likeRepository,
            IPostRepository repository,
            IFileStorage fileStorage)
        {
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
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

        public ServiceResult<Post> GetById(int postId)
        {
            var post = _postRepository
                .GetById(postId);

            if (post == null)
                return ServiceResult<Post>.Fail(ApiErrors.PostNotFound);

            return ServiceResult<Post>.Success(post);
        }

        public ServiceResult<Comment> AddComment(AddCommentRequest commentRequest)
        {
            using var scope = new TransactionScope();

            try
            {
                Comment newComment = CommentFactory.Create(commentRequest);

                _commentRepository.Add(newComment);

                scope.Complete();

                return ServiceResult<Comment>.Success(newComment);
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }

        public ServiceResult<Like> InteractWithPost(InteractWithPostRequest interactionRequest, ApplicationUser user)
        {
            using var scope = new TransactionScope();

            try
            {
                var exisitingInteraction = _likeRepository
                    .GetByPostAndUser(interactionRequest.PostId, user.Id);

                if (exisitingInteraction == null)
                {

                    Like newLike = LikeFactory.Create(interactionRequest);

                    _likeRepository.Add(newLike);

                    scope.Complete();

                    return ServiceResult<Like>.Success(newLike);
                }
                else
                {
                    switch ((interactionRequest.Upvoted, interactionRequest.DownVoted))
                    {
                        case (false, false):
                            _likeRepository.Delete(exisitingInteraction);
                            scope.Complete();

                            return ServiceResult<Like>.Success(null!);
 
                        case (true, false):
                            exisitingInteraction.Up = true;
                            _likeRepository.Update(exisitingInteraction);

                            break;

                        case (false, true):
                            exisitingInteraction.Up = false;
                            _likeRepository.Update(exisitingInteraction);

                            break;

                        case (true, true):
                            return ServiceResult<Like>.Fail(ApiErrors.WrongInteraction);
                    }

                    scope.Complete();
                    return ServiceResult<Like>.Success(exisitingInteraction);
                }
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }
        
        public ServiceResult<Like> InteractWithComment(InteractWithCommentRequest interactionRequest, ApplicationUser user)
        {
            using var scope = new TransactionScope();

            try
            {
                var exisitingInteraction = _likeRepository
                    .GetByCommentAndUser(interactionRequest.CommentId, user.Id);

                if (exisitingInteraction == null)
                {

                    Like newLike = LikeFactory.Create(interactionRequest);

                    _likeRepository.Add(newLike);

                    scope.Complete();

                    return ServiceResult<Like>.Success(newLike);
                }
                else
                {
                    switch ((interactionRequest.Upvoted, interactionRequest.DownVoted))
                    {
                        case (false, false):
                            _likeRepository.Delete(exisitingInteraction);
                            scope.Complete();

                            return ServiceResult<Like>.Success(null!);
 
                        case (true, false):
                            exisitingInteraction.Up = true;
                            _likeRepository.Update(exisitingInteraction);

                            break;

                        case (false, true):
                            exisitingInteraction.Up = false;
                            _likeRepository.Update(exisitingInteraction);

                            break;

                        case (true, true):
                            return ServiceResult<Like>.Fail(ApiErrors.WrongInteraction);
                    }

                    scope.Complete();
                    return ServiceResult<Like>.Success(exisitingInteraction);
                }
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }
    }
}
