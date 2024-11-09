namespace backend.apiModels
{
    public class LoginRequest
    {
        public string Login { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
