namespace Application.Interfaces
{
    public interface IFileStorage
    {
        public string WriteImageFile(string base64);

        public string ReadImageFile(string path);
    }
}
