using Microsoft.AspNetCore.Mvc;
using Expense.Tracker.API.Domain.DTO;
using Expense_Tracker_API_Application.Interfaces;

namespace ExpenseTracker_API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("auth/get-token", ([FromServices] IAuthService service, [FromBody] ValidateTokenDto request) =>
        {
            return service.ValidateUserAsync(request);
        });

        app.MapGet("auth/validate", () => Results.Accepted()).RequireAuthorization();
    }
}