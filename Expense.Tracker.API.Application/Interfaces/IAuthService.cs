using Expense.Tracker.API.Domain.DTO;

namespace Expense_Tracker_API_Application.Interfaces;

public interface IAuthService
{
    Task<string?> ValidateUserAsync(ValidateTokenDto request);

}