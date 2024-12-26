namespace Backend.ApiModels.Requests
{
    public class LoginRequest
    {
        public string Login { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
