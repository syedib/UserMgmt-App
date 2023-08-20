using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using UserMgmtApi.Exceptions;

namespace UserMgmtApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.GetType().IsSubclassOf(typeof(NotFoundException)) ? StatusCodes.Status404NotFound : StatusCodes.Status500InternalServerError;

            var response = new
            {
                Message = "An error occurred while processing your request.",
                ExceptionMessage = exception.Message,
                ExceptionType = exception.GetType().Name
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
