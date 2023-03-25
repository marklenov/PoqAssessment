using Newtonsoft.Json;
using PoqAssessment.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PoqAssessment.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, httpContext);
        }
    }

    private async Task HandleExceptionAsync(Exception ex, HttpContext httpContext)
    {
        var httpStatusCode = HttpStatusCode.InternalServerError;
        string response;

        switch (ex)
        {
            case AppException appException:
                _logger.LogWarning(appException, "Application exception was thrown.");

                httpStatusCode = HttpStatusCode.InternalServerError;

                response = JsonConvert.SerializeObject(new
                {
                    message = appException.Message,
                    errors = appException.Errors
                });

                break;
            case BadRequestException badRequest:
                _logger.LogWarning(badRequest, "Bad request exception was thrown.");

                httpStatusCode = HttpStatusCode.BadRequest;

                response = JsonConvert.SerializeObject(new
                {
                    message = badRequest.Message,
                    errors = new { }
                });

                break;
            default:
                _logger.LogError(ex, "Unhandled exception was thrown.");

                response = JsonConvert.SerializeObject(new
                {
                    message = /*_env.IsDevelopment() ? ex.Message :*/ "Internal Server Error",
                    errors = new { }
                });

                break;
        }

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)httpStatusCode;

        await httpContext.Response.WriteAsync(response);
    }
}
