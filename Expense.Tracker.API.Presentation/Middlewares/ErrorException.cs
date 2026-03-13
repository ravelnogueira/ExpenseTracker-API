using Expense.Tracker.API.CrossCutting;
using Expense.Tracker.API.Domain.ErrorCodes;

namespace Expense.Tracker.API.Presentation.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
        catch (AppException ex)
        {
            _logger.LogWarning(ex,
                "Application error occurred. ErrorCode: {ErrorCode}",
                ex.ErrorCode);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                ErrorCode = ex.ErrorCode,
                Message = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                ErrorCode = ErrorCodes.UnknownError,
                Message = "An unexpected error occurred."
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}