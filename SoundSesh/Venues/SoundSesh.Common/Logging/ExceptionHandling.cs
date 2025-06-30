using Microsoft.ApplicationInsights;
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
        private readonly TelemetryClient _telemetry = new TelemetryClient();

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Called whenever a method is called and catches exceptions if they occur
        /// </summary>
        public async Task Invoke(HttpContext context, IHostingEnvironment env, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env, _telemetry, logger);
            }
        }

        // https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostingEnvironment env, TelemetryClient telemetry, ILogger logger)
        {
            var requestData = new Dictionary<string, string>
            {
                ["RequestPath"] = context.Request.Path.Value,
                ["RequestQueryString"] = context.Request.QueryString.Value,
            };

            telemetry.TrackException(exception, requestData);
            logger.LogError(exception, "Unhandled Exception", requestData);

            var code = HttpStatusCode.InternalServerError;

            // Proof of concept to show different exceptions can be handled differently even with different status codes if need be
            ////if (exception is NotImplementedException)
            ////{
            ////    code = HttpStatusCode.BadRequest;
            ////}

            string result;
            if (env.IsDevelopment())
            {
                result = JsonConvert.SerializeObject(new
                {
                    Error = exception.Message,
                    exception.StackTrace,
                    Name = "Unhandled exception: " + exception.GetType().Name,
                    InnerException = exception?.InnerException
                });
            }
            else
            {
                result = JsonConvert.SerializeObject(new
                {
                    Errors = new Dictionary<string, string> { ["E500"] = "An error occurred." }
                });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
