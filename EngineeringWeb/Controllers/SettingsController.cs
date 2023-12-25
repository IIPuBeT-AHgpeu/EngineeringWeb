using EngineeringWeb.Services;
using EngineeringWeb.Settings;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        ISettingsManager _manager;
        ISimpleLogger _logger;
        public SettingsController(ISettingsManager manager, ISimpleLogger logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public Settings.Settings? GetSettings()
        {
            _logger.Log(LogLevel.Information, "Получение информации о настройках");
            return _manager.GetSettings();
        }

        [HttpPut]
        public IActionResult SetSettings([FromBody] Settings.Settings settings)
        {
            if (settings == null)
            {
                _logger.Log(LogLevel.Error, "При установке настроек сами настройки не были переданы.");
                return BadRequest("Тело запроса пустое!");               
            }

            try
            {
                _manager.SetSettings(settings);
                _logger.Log(LogLevel.Information, "Настройки успешно изменены.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
