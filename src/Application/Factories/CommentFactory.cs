using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Factories
{
    public class CommentFactory
    {
        public static Comment Create(AddCommentRequest request)
        {
            return new Comment()
            {
                AuthorId = request.AuthorId,
                OfferId = request.OfferId,
                PostId = request.PostId,
                ParentId = request.ParentId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
