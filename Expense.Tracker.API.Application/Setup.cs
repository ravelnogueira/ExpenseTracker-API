using Microsoft.Extensions.DependencyInjection;
using Expense_Tracker_API_Application.Services;
using Expense_Tracker_API_Application.Interfaces;


namespace Expense_Tracker_API_Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection  builder)
    {
        builder.AddScoped<IAuthService, AuthService>();
    }
    
}