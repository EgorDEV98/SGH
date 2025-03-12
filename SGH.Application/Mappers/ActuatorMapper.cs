using Riok.Mapperly.Abstractions;
using SGH.Application.Models.Responces;
using SGH.Data.Entities;

namespace SGH.Application.Mappers;

[Mapper]
public partial class ActuatorMapper
{
    public partial GetActuatorResponse Map(Actuator actuator);
    public partial IReadOnlyCollection<GetActuatorResponse> Map(IEnumerable<Actuator> actuator);
}