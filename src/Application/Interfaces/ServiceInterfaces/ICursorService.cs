using System.Text.Json;
using Application.Dtos.RequestDtos;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface ICursorService
    {
        public string EncodeCursor(OfferCursorDto cursor);

        public OfferCursorDto? DecodeCursor(string? value);
    }
}
