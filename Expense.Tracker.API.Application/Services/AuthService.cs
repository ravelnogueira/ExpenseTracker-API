using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Expense.Tracker.API.Application.DTO;
using Expense.Tracker.API.Application.Interfaces;
using Expense.Tracker.API.CrossCutting;
using Expense.Tracker.API.Domain.ErrorCodes;
using Expense.Tracker.API.Infrastructure.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Expense.Tracker.API.Application.Services;

public class AuthService : IAuthService
{
    private readonly AuthConfiguration _configuration;

    public AuthService(IOptions<AuthConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public Task<string> ValidateUserAsync(ValidateTokenDto request)
    {
        var isValid = request.Email == _configuration.UserName && request.Password == _configuration.PassWord;

        if (!isValid)
            throw new AppException(ErrorCodes.InvalidCredentials, "Invalid username or password");

        return GenerateToken(request.Email);
    }

    private Task<string> GenerateToken(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, "admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(tokenString);
    }
}