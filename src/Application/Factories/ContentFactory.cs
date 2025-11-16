using Application.Dtos.ModelDtos;
using Domain.Models;

namespace Application.Factories
{
    public static class ContentFactory
    {
        public static Content Create(ContentDto request)
        {
            return new Content()
            {
                PostId = request.PostId,
                OfferId = request.OfferId,
                Type = request.Type,
                TextContent = request.TextContent,
                Position = request.Position,
                GalleryElements = request.GalleryElements
                    .Select(ge => GalleryElementFactory.Create(ge))
                    .ToList(),
            };
        }
    }
}
