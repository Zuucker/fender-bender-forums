using Application.Interfaces;
using ImageMagick;
using Infrastructure.Persistence;

namespace Tests.UnitTests
{
    public class LocalFileStorageTests : IDisposable
    {
        private readonly string _tempDir;
        private readonly IFileStorage _storage;

        public LocalFileStorageTests()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            _storage = new LocalFileStorage(_tempDir);
        }

        [Fact]
        public void WriteImageFile_CreatesWebpFile_AndReturnsFileName()
        {
            var base64Image = CreateBase64Image();

            var fileName = _storage.WriteImageFile(base64Image);

            Assert.False(string.IsNullOrWhiteSpace(fileName));
            Assert.EndsWith(".webp", fileName);

            var fullPath = Path.Combine(_tempDir, fileName);
            Assert.True(File.Exists(fullPath));
            Assert.True(new FileInfo(fullPath).Length > 0);
        }

        [Fact]
        public void ReadImageFile_ReturnsFileBytes_WhenFileExists()
        {
            var base64Image = CreateBase64Image();
            var fileName = _storage.WriteImageFile(base64Image);

            var fileId = Guid.Parse(Path.GetFileNameWithoutExtension(fileName));
            var bytes = _storage.ReadImageFile(fileId);

            Assert.NotNull(bytes);
            Assert.NotEmpty(bytes);
        }

        [Fact]
        public void ReadImageFile_Throws_WhenFileDoesNotExist()
        {
            var nonExistentId = Guid.NewGuid();

            Assert.Throws<DirectoryNotFoundException>(() =>
                _storage.ReadImageFile(nonExistentId));
        }

        private static string CreateBase64Image()
        {
            using var image = new MagickImage(MagickColors.Red, 10, 10);
            image.Format = MagickFormat.Png;

            return Convert.ToBase64String(image.ToByteArray());
        }

        public void Dispose()
        {
            if (Directory.Exists(_tempDir))
                Directory.Delete(_tempDir, recursive: true);
        }
    }
}
