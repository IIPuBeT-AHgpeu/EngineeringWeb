using EngineeringWeb.Model;
using EngineeringWeb.Services;
using EngineeringWeb.Settings;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        ISettingsManager _manager;
        ISimpleLogger _logger;

        public IModel model;

        public ModelController(ISettingsManager manager, ISimpleLogger logger)
        {
            _manager = manager;
            model = new Model.Model(_manager);
            _logger = logger;
        }

        [HttpGet("info")]
        public Settings.ModelSettings? GetModelInfo()
        {
            var result = _manager.GetSettings();

            if (result == null)
            {
                _logger.Log(LogLevel.Error, "Не удалось получить информацию о настройках модели.");
                return null;
            }
            else 
            {
                _logger.Log(LogLevel.Information, "Были получены настройки модели.");
                return result.ModelSettings;
            }
        }

        [HttpGet("create")]
        public IActionResult CreateModel()
        {
            bool isCreated = model.CreateModel();

            if (!isCreated)
            {
                _logger.Log(LogLevel.Error, "Не удалось создать модель.");
                return StatusCode(404, "Не удалось создать модель.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Модель была создана.");
                return Ok();
            }
        }

        [HttpDelete]
        public IActionResult DeleteModel()
        {
            bool isCreated = model.DeleteModel();

            if (!isCreated)
            {
                _logger.Log(LogLevel.Error, "Не удалось удалить модель.");
                return StatusCode(404, "Не удалось удалить модель.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Модель была удалена.");
                return Ok();
            }
        }

        [HttpPost("check")]
        public IActionResult Check(IFormFile file)
        {
            var settings = _manager.GetSettings();
            if (settings == null)
            {
                _logger.Log(LogLevel.Error, "Файл настроек не найден.");
                return BadRequest("Нет файла настроек.");
            }
            if (!model.IsModelExist(settings.Path + "\\" + settings.ModelName))
            {
                _logger.Log(LogLevel.Error, "Не удалось найти файл модели.");
                return BadRequest("Нет модели.");
            }
            if (file == null)
            {
                _logger.Log(LogLevel.Error, "Картинка не была получена.");
                return BadRequest("Нет картинки.");
            }
            var result = model.UseModel(file);

            _logger.Log(LogLevel.Information, "Модель выдала результат: " + result);
            return Ok(result);
        }
    }
}
