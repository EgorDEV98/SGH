using Riok.Mapperly.Abstractions;
using SGH.Application.Models.Responces;
using SGH.Data.Entities;

namespace SGH.Application.Mappers;

[Mapper]
public partial class SensorMapper
{
   public partial GetSensorResponse Map(Sensor entity);
   public partial IReadOnlyCollection<GetSensorResponse> Map(IEnumerable<Sensor> entity);
   public partial IReadOnlyCollection<GetSensorValueResponse> Map(IEnumerable<SensorValue> entity);
}