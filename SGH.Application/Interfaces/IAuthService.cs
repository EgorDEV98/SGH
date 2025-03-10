using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;

namespace SGH.Application.Interfaces;

public interface IAuthService
{
    public Task<AuthResponse> Login(AuthParams param, CancellationToken ct);
    public Task<RegistrationResponse> Registration(RegistrationParams param, CancellationToken ct);
}