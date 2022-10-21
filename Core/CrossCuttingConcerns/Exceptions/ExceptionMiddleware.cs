using Core.Business.Exceptions;
using Core.CrossCuttingConcerns.Security.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        // Delegate, Action, Func

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception.GetType() == typeof(BusinessException))
                return CreateBusinessException(context, exception);
            if (exception.GetType() == typeof(ValidationException))
                return CreateValidationException(context, exception);
            if (exception.GetType() == typeof(AuthenticationException))
                return CreateAuthenticationException(context, exception);
            if (exception.GetType() == typeof(AuthorizeException))
                return CreateAuthorizeException(context, exception);

            return CreateInternalException(context, exception);
        }


        private Task CreateValidationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

            return context.Response.WriteAsync(new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://rentacar.com/api/docs/validation",
                Title = "Validation Error(s)",
                Detail = "",
                Instance = "",
                Errors = (exception as ValidationException)!.Errors
            }.ToString());
        }

        private Task CreateBusinessException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            // Casting (int)
            return context.Response.WriteAsync(new BusinessProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://rentacar.com/api/docs/business",
                Title = "Business Exception",
                Detail = exception.Message,
                Instance = ""
            }.ToString());
        }

        private Task CreateInternalException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://rentacar.com/api/docs/internal",
                Title = "Internal Exception",
                Detail = exception.Message,
                Instance = ""
            }));
        }

        private Task CreateAuthenticationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

            return context.Response.WriteAsync(new AuthenticationProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = "https://rentacar.com/api/docs/authentication",
                Title = "Authentication Error",
                Detail = exception.Message,
                Instance = "",
            }.ToString());
        }

        private Task CreateAuthorizeException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Forbidden);

            return context.Response.WriteAsync(new AuthorizationProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Type = "https://rentacar.com/api/docs/authorize",
                Title = "Authorize Error",
                Detail = exception.Message,
                Instance = "",
            }.ToString());
        }
    }
}
