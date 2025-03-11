using Riok.Mapperly.Abstractions;
using SGH.Application.Models.Responces;
using SGH.Data.Entities;

namespace SGH.Application.Mappers;

[Mapper]
public partial class DeviceMapper
{
    public partial GetDeviceResponse Map(Device device);
    public partial IReadOnlyCollection<GetDeviceResponse> Map(IEnumerable<Device> device);
}