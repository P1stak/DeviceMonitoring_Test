using DeviceMonitoring.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace DeviceMonitoring.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ConcurrentDictionary<Guid, Device> _devices = new();
        private readonly ILogger<DeviceRepository> _logger;

        public DeviceRepository(ILogger<DeviceRepository> logger)
        {
            _logger = logger;
        }

        public Task AddDeviceAsync(Device device)
        {
            try
            {
                if (string.IsNullOrEmpty(device.Id.ToString()))
                {
                    device.Id = Guid.NewGuid();
                }

                _devices.TryAdd(device.Id, device);
                _logger.LogInformation($"Добавлено устройство - {device.Id}");

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return Task.FromResult(_devices.Values.AsEnumerable());
        }

        public Task<IEnumerable<Device>> GetTryByIdAsunc(Guid id)
        {
            var device = _devices.Values.Where(x => x.Id == id).ToList();

            if (!device.Any())
            {
                _logger.LogWarning($"Не найдено устройство {id}");
            }

            return Task.FromResult(device.AsEnumerable());
        }

        public Task ClearOldRecordsAsync(DateTime date)
        {
            var oldRecords = _devices.Where(x => x.Value.EndTime < date).ToList();

            foreach (var item in oldRecords)
            {
                _devices.TryRemove(item.Key, out _);
            }

            _logger.LogInformation($"Кол-во удаленных старых сессий - {oldRecords.Count}");

            return Task.CompletedTask;
        }

        public async Task BackUpDataAsunc(string filePath)
        {
            try
            {
                var json = JsonSerializer.Serialize(_devices.Values);
                await File.WriteAllTextAsync(filePath, json);
                _logger.LogInformation($"Резервная копия создана - {filePath}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message} Ошибка создания бекапа");
                throw;
            }
        }
    }
}
