using Expense.Tracker.API.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Expense.Tracker.API.Application.Services;

namespace Expense.Tracker.API.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection  builder)
    {
        builder.AddScoped<IAuthService, AuthService>();
    }
    
}