using Application.Common;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Responses
{
    public class ResponseHelper
    {
        public static IActionResult PrepareResponse<T>(ServiceResult<T?> result)
        {
            if (result.Data != null)
            {
                ApiResponseDto<T> responseData = new() { Data = result.Data };

                return new OkObjectResult(responseData);
            }

            var errorCode = ApiResponseMapper.ServiceErrorToHttpCode(result.Error);

            ApiResponseDto<T> errorResponseData = new() { Error = result.Error.ToString() };

            return new ObjectResult(errorResponseData) { StatusCode = (int)errorCode };
        }

        public static IActionResult PrepareResponse<T>(T data)
        {
            ApiResponseDto<T> responseData = new() { Data = data };

            return new OkObjectResult(responseData);
        }

        public static IActionResult PrepareResponse(ApiErrors error)
        {
            var errorCode = ApiResponseMapper.ServiceErrorToHttpCode(error);

            ApiResponseDto<object> errorResponseData = new() { Error = error.ToString() };

            return new ObjectResult(errorResponseData) { StatusCode = (int)errorCode };
        }
    }
}
