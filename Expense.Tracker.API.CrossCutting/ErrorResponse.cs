namespace Expense.Tracker.API.CrossCutting;

public sealed class ErrorResponse
{
    public string ErrorCode { get; init; } = default!;
    public string Message { get; init; } = default!;
    public object? Details { get; init; }
}