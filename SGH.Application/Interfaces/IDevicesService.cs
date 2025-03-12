using SGH.Application.Models.Params;
using SGH.Application.Models.Responces;

namespace SGH.Application.Interfaces;

public interface IDevicesService
{
    public Task<GetDeviceResponse> GetDevice(GetDeviceParams param, CancellationToken ct);
    public Task<IReadOnlyCollection<GetDeviceResponse>> GetDevices(GetDevicesParams param, CancellationToken ct);
    public Task<GetDeviceResponse> AddDevice(AddDeviceParams param, CancellationToken ct);
    public Task<GetDeviceResponse> UpdateDevice(UpdateDeviceParams param, CancellationToken ct);
    public Task<bool> DeleteDevice(DeleteDeviceParams param, CancellationToken ct);
}