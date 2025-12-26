using Domain.Errors;
using System.Diagnostics.CodeAnalysis;

namespace Application.Common
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; } = default!;

        public ApiErrors? Error { get; set; }


        private ServiceResult() { }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>()
            {
                Data = data,
            };
        }

        public static ServiceResult<T> Fail(ApiErrors error)
        {
            return new ServiceResult<T>()
            {
                Error = error,
            };
        }

        [MemberNotNullWhen(false, nameof(Data))]
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
