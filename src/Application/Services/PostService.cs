using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICursorService _cursorService;
        private readonly IFileStorage _fileStorage;
        private readonly int _defaultPageSize = 10;

        public PostService(
            ICommentRepository commentRepository,
            ILikeRepository likeRepository,
            IPostRepository repository,
            ICursorService cursorService,
            IFileStorage fileStorage)
        {
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _postRepository = repository;
            _cursorService = cursorService;
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

        public ServiceResult<(IEnumerable<Post>, string?)> GetFilteredPosts(FiltersDto filters, string? cursor)
        {
            using var scope = new TransactionScope();

            try
            {
                var query = _postRepository
                    .GetPostsQuery();


                // Filtering
                if (!string.IsNullOrWhiteSpace(filters.Title))
                    query = query.Where(x => x.Post.Title.Contains(filters.Title));

                if (filters.CarId.HasValue)
                    query = query.Where(x => x.Post.CarId == filters.CarId.Value);

                if (!string.IsNullOrWhiteSpace(filters.AuthorId))
                    query = query.Where(x => x.Post.AuthorId == filters.AuthorId);

                if (!string.IsNullOrWhiteSpace(filters.Tags))
                {
                    var tagTexts = filters.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var jsonConditions = tagTexts.Select(tag => $"[{{\"Text\":\"{tag}\"}}]").ToList();

                    query = query.Where(x =>
                        jsonConditions.Any(jsonCondition =>
                            EF.Functions.JsonContains(x.Post.Tags, jsonCondition)
                        )
                        || (x.Post.Car != null && tagTexts.Any(tag =>
                            x.Post.Car.Model.Contains(tag)
                            || x.Post.Car.Manufacturer.Contains(tag)
                            || x.Post.Car.Type.Contains(tag)
                        ))
                    );
                }

                if (filters.SectionId.HasValue)
                    query = query.Where(x => x.Post.SectionId == filters.SectionId.Value);

                if (filters.CreationDate.HasValue)
                    query = query.Where(x => x.Post.CreationDate >= filters.CreationDate.Value);



                var decodedCursor =
                    _cursorService.DecodeCursor(cursor);

                query = query
                    .OrderByDescending(x => x.Post.CreationDate)
                    .ThenByDescending(o => o.LikeCount)
                    .ThenByDescending(x => x.Post.Id);

                if (decodedCursor != null)
                {
                    query = query.Where(x =>
                       x.Post.CreationDate < decodedCursor.CreatedAt
                       || (x.Post.CreationDate == decodedCursor.CreatedAt && x.LikeCount < decodedCursor.LikeCount)
                       || (x.Post.CreationDate == decodedCursor.CreatedAt
                           && x.LikeCount == decodedCursor.LikeCount
                           && x.Post.Id < decodedCursor.PostId)
                   );
                }

                var items = query
                    .Take(decodedCursor?.Take ?? _defaultPageSize)
                    .AsNoTracking()
                    .ToList();

                var nextCursor = items.Count == (decodedCursor?.Take ?? _defaultPageSize)
                    ? _cursorService.EncodeCursor(new OfferCursorDto
                    {
                        CreatedAt = items[^1].Post.CreationDate,
                        LikeCount = items[^1].LikeCount,
                        PostId = items[^1].Post.Id
                    })
                    : null;


                var posts = items
                    .Select(i => i.Post);


                return ServiceResult<(IEnumerable<Post>, string?)>.Success((posts, nextCursor));
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }
    }
}
