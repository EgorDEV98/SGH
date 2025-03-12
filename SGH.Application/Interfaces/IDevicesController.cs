using SGH.Application.Models.Request;
using SGH.Application.Models.Responces;

namespace SGH.Application.Interfaces;

public interface IDevicesController
{
    public Task<GetDeviceResponse> GetDevice(Guid id, CancellationToken ct);
    public Task<IReadOnlyCollection<GetDeviceResponse>> GetDevices(CancellationToken ct);
    public Task<GetDeviceResponse> AddDevice(AddDeviceRequest param, CancellationToken ct);
    public Task<GetDeviceResponse> UpdateDevice(Guid id, UpdateDeviceRequest param, CancellationToken ct);
    public Task DeleteDevice(Guid id, CancellationToken ct);
}