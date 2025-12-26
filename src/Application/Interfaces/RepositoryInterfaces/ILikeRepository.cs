using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface ILikeRepository : IRepository<Like, int>
    {
        public Like? GetByPostAndUser(int postiId, string userId);

        public Like? GetByCommentAndUser(int commentId, string userId);
    }
}
