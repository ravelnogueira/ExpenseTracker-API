namespace Expense.Tracker.API.Infrastructure.Auth;

public class AuthConfiguration
{
    public string SecretKey { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string PassWord { get; set; } = null!;
}