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
            ApiErrors.Unauthorized => HttpStatusCode.Unauthorized,
            ApiErrors.FileNotFound => HttpStatusCode.NotFound,
            ApiErrors.WrongInteraction => HttpStatusCode.Conflict,

            _ => HttpStatusCode.InternalServerError
        };
    }
}
