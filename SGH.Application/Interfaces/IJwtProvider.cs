using SGH.Data.Entities;

namespace SGH.Application.Interfaces;

public interface IJwtProvider
{
    public string GenerateJwtToken(User? user);
}