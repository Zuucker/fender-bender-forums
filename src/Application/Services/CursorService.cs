using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class CursorService : ICursorService
    {
        public string EncodeCursor(OfferCursorDto cursor)
        {
            var json = JsonSerializer.Serialize(cursor);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }

        public OfferCursorDto? DecodeCursor(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            string json = Encoding.UTF8.GetString(Convert.FromBase64String(value));
            return JsonSerializer.Deserialize<OfferCursorDto>(json)!;
        }
    }
}
