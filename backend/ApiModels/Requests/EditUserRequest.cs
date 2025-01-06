namespace Backend.ApiModels.Requests
{
    public class EditUserRequest
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email {  get; set; } = string.Empty;

        //TODO avatar
    }
}
