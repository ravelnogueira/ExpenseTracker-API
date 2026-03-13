namespace Expense.Tracker.API.Domain.ErrorCodes;

public static class ErrorCodes
{
    public const string UnknownError = "UNKNOWN_ERROR";
    public const string InvalidCredentials = "INVALID_CREDENTIALS";
    public const string ValidationError = "VALIDATION_ERROR";
    public const string ResourceNotFound = "RESOURCE_NOT_FOUND";
    public const string Unauthorized = "UNAUTHORIZED";
    public const string Forbidden = "FORBIDDEN";
    public const string Conflict = "CONFLICT";
}