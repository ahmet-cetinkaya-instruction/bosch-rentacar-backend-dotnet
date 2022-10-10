using System.Net;
using Core.Business.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            return CreateInternalException(context, exception);
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
    }
}
