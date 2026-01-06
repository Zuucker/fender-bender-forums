using Application.Interfaces;
using System.Buffers.Text;

namespace Infrastructure.Persistence
{
    public class LocalFileStorage : IFileStorage
    {
        private string _basePath;

        public LocalFileStorage(string basePath)
        {
            _basePath = basePath;
        }

        public string WriteImageFile(string base64)
        {
            try
            {
                if (!Directory.Exists(_basePath))
                    Directory.CreateDirectory(_basePath);

                string newFileName = string.Empty;
                string filePath = string.Empty;


                while (true)
                {
                    newFileName = $"{Guid.NewGuid()}.webp";
                    filePath = Path.Combine(_basePath, newFileName);

                    if (!File.Exists(filePath))
                        break;
                }

                var fileBytes = Convert.FromBase64String(base64);

                using var magickImage = new ImageMagick.MagickImage(fileBytes);

                magickImage.Format = ImageMagick.MagickFormat.WebP;
                magickImage.Resize(800, 0);

                File.WriteAllBytes(filePath, magickImage.ToByteArray());


                return newFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public byte[] ReadImageFile(Guid fileId)
        {
            try
            {
                var path = Path.Combine(_basePath, $"{fileId}.webp");
                var bytes = File.ReadAllBytes(path);

                return bytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
