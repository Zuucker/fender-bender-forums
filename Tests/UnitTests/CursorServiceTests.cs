using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Application.Services;

namespace Tests.UnitTests
{
    public class CursorServiceTests
    {
        private readonly ICursorService _cursorService;

        public CursorServiceTests()
        {
            _cursorService = new CursorService();
        }

        [Fact]
        public void EncodeCursor_ReturnsBase64String()
        {
            var cursor = new OfferCursorDto
            {
                CreatedAt = DateTime.UtcNow,
                LikeCount = 5,
                OfferId = 10,
                PostId = 20,
                Take = 30
            };

            var encoded = _cursorService.EncodeCursor(cursor);

            Assert.False(string.IsNullOrWhiteSpace(encoded));
            Assert.DoesNotContain("{", encoded);
            Assert.DoesNotContain("}", encoded);
        }

        [Fact]
        public void DecodeCursor_ReturnsEquivalentObject_WhenValueIsValid()
        {
            var cursor = new OfferCursorDto
            {
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                LikeCount = 12,
                OfferId = 3,
                PostId = 7,
                Take = 25
            };

            var encoded = _cursorService.EncodeCursor(cursor);
            var decoded = _cursorService.DecodeCursor(encoded);

            Assert.NotNull(decoded);
            Assert.Equal(cursor.CreatedAt, decoded!.CreatedAt);
            Assert.Equal(cursor.LikeCount, decoded.LikeCount);
            Assert.Equal(cursor.OfferId, decoded.OfferId);
            Assert.Equal(cursor.PostId, decoded.PostId);
            Assert.Equal(cursor.Take, decoded.Take);
        }

        [Fact]
        public void DecodeCursor_ReturnsNull_WhenValueIsNull()
        {
            var result = _cursorService.DecodeCursor(null);

            Assert.Null(result);
        }

        [Fact]
        public void DecodeCursor_ReturnsNull_WhenValueIsEmpty()
        {
            var result = _cursorService.DecodeCursor(string.Empty);

            Assert.Null(result);
        }

        [Fact]
        public void DecodeCursor_ThrowsFormatException_WhenValueIsNotBase64()
        {
            Assert.Throws<FormatException>(() =>
                _cursorService.DecodeCursor("not-base64"));
        }
    }
}
