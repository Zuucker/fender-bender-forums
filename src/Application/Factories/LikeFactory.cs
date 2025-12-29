using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Factories
{
    public class LikeFactory
    {
        public static Like Create(InteractWithPostRequest request)
        {
            return new Like()
            {
                AuthorId = request.AuthorId,
                PostId = request.PostId,
                Up = request.Upvoted && !request.DownVoted
            };
        }
        public static Like Create(InteractWithOfferRequest request)
        {
            return new Like()
            {
                AuthorId = request.AuthorId,
                OfferId = request.OfferId,
                Up = request.Upvoted && !request.DownVoted
            };
        }

        public static Like Create(InteractWithCommentRequest request)
        {
            return new Like()
            {
                AuthorId = request.AuthorId,
                CommentId = request.CommentId,
                Up = request.Upvoted && !request.DownVoted
            };
        }
    }
}
