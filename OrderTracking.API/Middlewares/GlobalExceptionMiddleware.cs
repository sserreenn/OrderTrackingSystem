using OrderTracking.Core.DTOs.Common;
using System.Net;

namespace OrderTracking.API.Middlewares;
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Sistemde bir hata oluştu: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var isBusinessError = exception.Message.Contains("sipariş") || exception.Message.Contains("müşteri");

        context.Response.StatusCode = isBusinessError
            ? (int)HttpStatusCode.BadRequest
            : (int)HttpStatusCode.InternalServerError;

        var response = new ErrorResponseDto
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Details = isBusinessError ? null : exception.StackTrace 
        };

        return context.Response.WriteAsync(response.ToString());
    }
}