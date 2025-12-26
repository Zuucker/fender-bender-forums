using Application.Dtos.ModelDtos;
using Domain.Models;

namespace Application.Factories
{
    public class GalleryElementFactory
    {
        public static GalleryElement Create(GalleryElementDto request)
        {
            return new GalleryElement()
            {
                Path = request.Path,
                GalleryPosition = request.GalleryPosition,
                ContentId = request.ContentId,
            };
        }
    }
}
