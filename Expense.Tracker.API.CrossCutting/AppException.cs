namespace Expense.Tracker.API.CrossCutting;

public sealed class AppException : Exception
{
    public string ErrorCode { get; }

    public AppException(string errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}