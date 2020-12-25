using kplus_app.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace kplus_app
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            var code = HttpStatusCode.InternalServerError;
            string detail = null;

            if (ex is NotValidException)
            {
                code = HttpStatusCode.BadRequest;
                detail = (ex as NotValidException).FieldName;
            }

            //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is MyException) code = HttpStatusCode.BadRequest;
            // 500 if unexpected

                if (!context.Response.HasStarted)
            {
                _logger.LogError(ex.Message +
               ex.StackTrace);

            }
            var result = JsonConvert.SerializeObject(new
                ProblemDetails
            {
                Type ="https://developer.mozilla.org/ru/docs/web/HTTP/Status",
               // ErrorLevel = ErrorLevel.Error,
                Status = (int)code,
                Title = ex.Message,
                Detail = detail,
                Instance = context.Request.Path,
               

            }

                );

           // context.Items["Error"] = result;
            context.Response.ContentType = "application/problem+json";

            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
