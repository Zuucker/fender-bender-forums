namespace Presentation.Responses
{
    public class ApiResponseDto<T>
    {
        public T? Data { get; set; }

        public string? Error { get; set; }

        public ApiResponseDto<T> Create(T data) => new ApiResponseDto<T> { Data = data };
    }
}
