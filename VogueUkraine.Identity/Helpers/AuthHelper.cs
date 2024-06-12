using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VogueUkraine.Identity.Data.Entities;
using VogueUkraine.Identity.Models;
using VogueUkraine.Identity.Options;

namespace VogueUkraine.Identity.Helpers;

public static class AuthHelper
{
    public static bool VerifyPasswordHash(string password, string storedHash)
    {
        using var sha256 = SHA256.Create();
        var computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computedHashString = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

        return computedHashString == storedHash;
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();
    }

    public static string GenerateAccessToken(GenerateAccessTokenModel model, JwtOptions jwtOptions)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserId),
                new Claim(ClaimTypes.Name, model.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(jwtOptions.ExpiresInMinutes),
            Issuer = jwtOptions.Issuer,
            Audience = jwtOptions.Audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}