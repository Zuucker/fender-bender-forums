using Domain.Errors;

namespace Application.Common
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }

        public ApiErrors? Error { get; set; }


        private ServiceResult(T? data, ApiErrors? error)
        {
            Data = data;
            Error = error;
        }

        public static ServiceResult<T> Success(T data) => new(data, null);

        public static ServiceResult<T> Failure(ApiErrors error) => new(default, error);

        public bool HasFailed()
        {
            return Error.HasValue;
        }

        public bool HasError(ApiErrors error)
        {
            return Error == error;
        }
    }
}
