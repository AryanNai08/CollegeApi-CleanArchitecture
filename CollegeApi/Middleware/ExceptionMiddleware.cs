using CollegeApi.Application.Common;
using CollegeApi.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using System.Text.Json;

namespace CollegeApi.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                HttpStatusCode statusCode;

                switch (ex)
                {
                    case NotFoundException:
                        statusCode = HttpStatusCode.NotFound;
                        break;

                    case BadRequestException:
                        statusCode = HttpStatusCode.NotFound;
                        break;

                    case UnauthorizedException:
                        statusCode = HttpStatusCode.NotFound;
                        break;

                    default:
                        statusCode = HttpStatusCode.InternalServerError;
                        break;
                }

                if (statusCode == HttpStatusCode.InternalServerError)
                {
                    _logger.LogError(ex, ex.Message);
                }
                else
                {
                    _logger.LogWarning(ex.Message);
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;

                var apiResponse = new APIResponse
                {
                    Status = false,
                    StatusCode = statusCode
                };

                apiResponse.Error.Add(ex.Message);

                var jsonResponse = JsonSerializer.Serialize(apiResponse);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}