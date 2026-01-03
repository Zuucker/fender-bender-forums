using Application.Dtos;
using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IPostRepository : IRepository<Post, int>
    {
        public IEnumerable<Post> GetUsersPosts(Guid userId);

        public IQueryable<PostQuery> GetPostsQuery();
    }
}
