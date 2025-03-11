using Riok.Mapperly.Abstractions;
using SGH.Application.Models.Responces;
using SGH.Data.Entities;

namespace SGH.Application.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial GetUserInfoResponse Map(User user);
}