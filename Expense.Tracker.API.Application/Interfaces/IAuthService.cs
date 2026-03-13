using Expense.Tracker.API.Application.DTO;

namespace Expense.Tracker.API.Application.Interfaces;

public interface IAuthService
{
    Task<string> ValidateUserAsync(ValidateTokenDto request);

}