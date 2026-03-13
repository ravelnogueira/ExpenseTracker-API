using Expense.Tracker.API.Application.DTO;
using Expense.Tracker.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expense.Tracker.API.Presentation.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("auth/get-token", ([FromServices] IAuthService service, [FromBody] ValidateTokenDto request)
            =>
        {
            var response = service.ValidateUserAsync(request);
            return Results.Ok(new { accessToken = response.Result });
        });

        app.MapGet("auth/validate", () => Results.Ok)
            .RequireAuthorization();
    }
}