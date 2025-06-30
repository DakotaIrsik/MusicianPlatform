using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SoundSesh.Common.Logging
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IHostingEnvironment env, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env, logger);
            }
        }

        //https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostingEnvironment env, ILogger logger)
        {
            var requestData = new Dictionary<string, string>
            {
                ["RequestPath"] = context.Request.Path.Value,
                ["RequestQueryString"] = context.Request.QueryString.Value,
            };

            logger.LogError(exception, "Unhandled Exception", requestData);

            string result;
            if (env.IsDevelopment())
            {
                result = JsonConvert.SerializeObject(new
                {
                    Error = exception.Message,
                    exception.StackTrace,
                    Name = "Unhandled exception: " + exception.GetType().Name,
                    exception?.InnerException
                });
            }
            else
            {
                result = JsonConvert.SerializeObject(new
                {
                    Errors = new Dictionary<string, string> { ["Error"] = "An error occurred." }
                });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }

}

