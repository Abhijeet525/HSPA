using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Errors;

namespace WebAPI.Middlewares
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionsMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ApiError response;
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

                string message;
                var exceptionType = ex.GetType();
                
                if(exceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                    message = "You are NOT Authorized";
                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Unknown Error Occured";
                }

                if (env.IsDevelopment())
                {
                    response = new ApiError((int)statusCode, ex.Message, ex.StackTrace);
                }
                else
                {
                    response = new ApiError((int)statusCode, message);
                }

                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
