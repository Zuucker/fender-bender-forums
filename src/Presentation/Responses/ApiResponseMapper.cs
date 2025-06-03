using Domain.Errors;
using System.Net;

namespace Presentation.Responses
{
    public class ApiResponseMapper
    {
        public static HttpStatusCode ServiceErrorToHttpCode(ApiErrors? error) => error switch
        {
            ApiErrors.UserNotFound => HttpStatusCode.NotFound,
            ApiErrors.ProvidedWrongPassword => HttpStatusCode.Unauthorized,
            ApiErrors.EmailAlreadyInUse => HttpStatusCode.BadRequest,
            ApiErrors.UsernameAlreadyInUse => HttpStatusCode.BadRequest,
            ApiErrors.PasswordsDontMatch => HttpStatusCode.BadRequest,

            _ => HttpStatusCode.InternalServerError
        };
    }
}
