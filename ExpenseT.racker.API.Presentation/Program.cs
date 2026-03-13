using System.Text;
using ExpenseTracker_API.Endpoints;
using Microsoft.IdentityModel.Tokens;
using Expense_Tracker_API_Application;
using Expense.Tracker.API.Domain.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AuthConfiguration:SecretKey")!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    });

builder.Services.Configure<AuthConfiguration>(configuration.GetSection("AuthConfiguration"));

builder.Services.AddAuthorization();

builder.Services.AddApplicationServices();

var app = builder.Build();  

app.UseAuthentication();
app.UseAuthorization(); 

app.MapAuthEndpoints();

await app.RunAsync();