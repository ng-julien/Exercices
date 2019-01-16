namespace Demo.Api.Core
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Application.Exceptions;

    using FluentValidation;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> logger;

        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = loggerFactory?.CreateLogger<ExceptionMiddleware>()
                      ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (ValidationException ex)
            {
                if (context.Response.HasStarted)
                {
                    this.logger.LogWarning(
                        "The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // perdu mon context de donnée
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NotFoundException ex)
            {
                if (context.Response.HasStarted)
                {
                    this.logger.LogWarning(
                        "The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}