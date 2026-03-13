using System.Text;
using Expense.Tracker.API.Application;
using Microsoft.IdentityModel.Tokens;
using Expense.Tracker.API.Infrastructure.Auth;
using Expense.Tracker.API.Presentation.Endpoints;
using Expense.Tracker.API.Presentation.Middlewares;
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

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapAuthEndpoints();

await app.RunAsync();