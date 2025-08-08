using DeviceMonitoring.Models;

namespace DeviceMonitoring.Repository
{
    public interface IDeviceRepository
    {
        Task AddDeviceAsync(Device device);
        Task BackUpDataAsunc(string filePath);
        Task ClearOldRecordsAsync(DateTime date);
        Task<IEnumerable<Device>> GetAllDevicesAsync();
        Task<IEnumerable<Device>> GetTryByIdAsunc(Guid id);
    }
}