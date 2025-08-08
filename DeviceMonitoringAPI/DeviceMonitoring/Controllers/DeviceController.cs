using DeviceMonitoring.Models;
using DeviceMonitoring.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeviceMonitoring.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ILogger<DeviceController> _logger;
        public DeviceController(IDeviceRepository deviceRepository, ILogger<DeviceController> logger)
        {
            _deviceRepository = deviceRepository;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Device device)
        {
            try
            {
                if (device == null)
                {
                    return BadRequest("Нет данных об устройстве");
                }

                if (string.IsNullOrEmpty(device.Name))
                {
                    return BadRequest("Нет имени девайса");
                }

                await _deviceRepository.AddDeviceAsync(device);

                _logger.LogInformation($"Устройство успешно добавлено - {device.Id}");

                return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ошибка добавленя устройства");
                return StatusCode(500, "Ошибка сервера");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var devices = await _deviceRepository.GetAllDevicesAsync();

                _logger.LogInformation($"Получено {devices.Count()} устройств");

                return Ok(devices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ошибка получения устройств");

                return StatusCode(500, "Ошибка сервера");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(Guid id)
        {
            try
            {
                var devices = await _deviceRepository.GetTryByIdAsunc(id);

                if (!devices.Any())
                {
                    _logger.LogInformation($"Не найдено устройство - {id}");

                    return NotFound();
                }
                return Ok(devices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Ошибка получения устройства {id}");

                return StatusCode(500, "Ошибка сервера");
            }
        }

        [HttpDelete("clear-old-records")]
        public async Task<IActionResult> ClearOldRecords([FromBody] DateTime date)
        {
            try
            {
                await _deviceRepository.ClearOldRecordsAsync(date);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ошибка удаления старой сессии");

                return StatusCode(500, "Ошибка сервера");
            }
        }

        [HttpPost("backup")]
        public async Task<IActionResult> CreateBackUp([FromBody] string filePath = "backup.json")
        {
            try
            {
                await _deviceRepository.BackUpDataAsunc(filePath);

                var result = new 
                { 
                    Message = $"Копия создана в файл {filePath}" 
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ошибка создания резервной копии");

                return StatusCode(500, "Ошибка сервера");

            }
        }


    }
}
