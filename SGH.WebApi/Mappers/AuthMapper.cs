using Riok.Mapperly.Abstractions;
using SGH.Application.Models.Params;
using SGH.Application.Models.Request;

namespace SGH.WebApi.Mappers;

[Mapper]
public partial class AuthMapper
{
    public partial AuthParams Map(AuthRequest authRequest);
    public partial RegistrationParams Map(RegistrationRequest registrationRequest);
}