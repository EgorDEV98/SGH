using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SGH.Application.Interfaces;
using SGH.Application.Models.JWT;
using SGH.Data.Entities;

namespace SGH.Application.Common;

public class JwtProvider : IJwtProvider
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtProvider(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateJwtToken(User? user)
    {
        if (user is null)  throw new ArgumentNullException("User is null");
        
        var nowDate = _dateTimeProvider.GetCurrent();
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: nowDate,
            claims: new List<Claim>()
            {
                new Claim(nameof(User.Id), user!.Id.ToString()),
                new Claim(nameof(User.Name), user.Name)
            },
            expires: nowDate.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}