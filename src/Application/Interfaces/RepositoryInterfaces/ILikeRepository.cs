using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ILikeRepository : IRepository<Like, int>
    {
        public Like? GetByPostAndUser(int postId, string userId);

        public Like? GetByOfferAndUser(int offerId, string userId);

        public Like? GetByCommentAndUser(int commentId, string userId);
    }
}
