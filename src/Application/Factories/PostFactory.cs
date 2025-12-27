using Application.Dtos.RequestDtos;
using Domain.Models;

namespace Application.Factories
{
    public static class PostFactory
    {
        public static Post Create(AddPostRequest request)
        {
            return new Post()
            {
                AuthorId = request.AuthorId,
                SectionId = request.SectionId,
                Title = request.Title,
                CreationDate = request.CreationDate,
                Content = request.Content,
                Tags = request.Tags,
                CarId = request.CarId,
                Contents = request.Contents
                    .Select(ac => ContentFactory.Create(ac))
                    .ToList()
            };
        }
    }
}
