using Application.Dtos.ModelDtos;
using Domain.Models;

namespace Application.Factories
{
    public static class AdditionalContentFactory
    {
        public static AdditionalContent Create(AdditionalContentDto request)
        {
            return new AdditionalContent()
            {
                PostId = request.PostId,
                OfferId = request.OfferId,
                Type = request.Type,
                Content = request.Content,
                Path = request.Path
            };
        }
    }
}
