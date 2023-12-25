using EngineeringWeb.Services;
using EngineeringWeb.Settings;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : Controller
    {
        ISettingsManager _manager;
        IDataManager _dm;
        ISimpleLogger _logger;

        public DataController(ISettingsManager manager, IDataManager dm, ISimpleLogger logger)
        {
            _manager = manager;           
            _dm = dm;
            _logger = logger;
        }

        [HttpDelete("dirclear/{level}")]
        public IActionResult Clear([FromRoute] int level)
        {
            if (level < 0 || level > 4)
            {
                _logger.Log(LogLevel.Error, "Неверные входные данные.");
                return BadRequest("Неверные входные данные");
            }    
                

            var settings = _manager.GetSettings();

            if (settings == null)
            {
                _logger.Log(LogLevel.Error, "Не найден файл настроек.");
                return NotFound("Не найден файл настроек.");
            }

            _dm.SetPath(settings!.Path);

            bool result = _dm.Clear(level);

            if (result)
            {
                _logger.Log(LogLevel.Information, "Директория была очищена.");
                return Ok();
            }
            else
            {
                _logger.Log(LogLevel.Error, "Ошибка при очистке директории.");
                return BadRequest("Ошибка при очистке директории.");
            }
        }

        [HttpGet("count/{level}")]
        public IActionResult GetCount([FromRoute] int level)
        {
            if (level < 0 || level > 4)
            {
                _logger.Log(LogLevel.Error, "Неверные входные данные.");
                return BadRequest("Неверные входные данные");
            }

            var settings = _manager.GetSettings();

            if (settings == null)
            {
                _logger.Log(LogLevel.Error, "Не найден файл настроек.");
                return NotFound("Не найден файл настроек.");
            }

            _dm.SetPath(settings!.Path);

            int result = _dm.CountPhotos(level);

            _logger.Log(LogLevel.Information, "Количество файлов в директории уровня " + level + " составляет: " + result);
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult AddPhoto(IFormFile data, [FromQuery] int level, [FromQuery] string filename, [FromQuery] string format)
        {
            if (data == null || level < 0 || level > 4 || filename == null || filename == String.Empty || format == null || format == String.Empty)
            {
                _logger.Log(LogLevel.Error, "Неверные входные данные.");
                return BadRequest("Неверные входные данные");
            }

            var settings = _manager.GetSettings();

            if(settings == null)
            {
                _logger.Log(LogLevel.Error, "Не найден файл настроек.");
                return NotFound("Не найден файл настроек.");
            }

            _dm.SetPath(settings!.Path);

            bool result = _dm.SaveData(data, level, filename, format);

            if (result)
            {
                _logger.Log(LogLevel.Information, "Фото было добавлено.");
                return Ok();
            }
            else
            {
                _logger.Log(LogLevel.Error, "Ошибка при добавлении фото.");
                return BadRequest("Ошибка при добавлении фото.");
            }
        }

        [HttpDelete("delete")]
        public IActionResult DeletePhoto([FromQuery] int level, [FromQuery] string filename, [FromQuery] string format)
        {
            if (level < 0 || level > 4 || filename == null || filename == String.Empty || format == null || format == String.Empty)
            {
                _logger.Log(LogLevel.Error, "Неверные входные данные.");
                return BadRequest("Неверные входные данные");
            }

            var settings = _manager.GetSettings();

            if (settings == null)
            {
                _logger.Log(LogLevel.Error, "Не найден файл настроек.");
                return NotFound("Не найден файл настроек.");
            }

            _dm.SetPath(settings!.Path);

            bool result = _dm.DeleteData(filename, level, format);

            if (result)
            {
                _logger.Log(LogLevel.Information, "Фото было удалено.");
                return Ok();
            }
            else
            {
                _logger.Log(LogLevel.Error, "Ошибка при удалении фото.");
                return BadRequest("Ошибка при удалении фото.");
            }
        }

        [HttpGet("exist")]
        public IActionResult IsExist([FromQuery] int level, [FromQuery] string filename, [FromQuery] string format)
        {
            if (level < 0 || level > 4 || filename == null || filename == String.Empty || format == null || format == String.Empty)
            {
                _logger.Log(LogLevel.Error, "Неверные входные данные.");
                return BadRequest("Неверные входные данные");
            }

            var settings = _manager.GetSettings();

            if (settings == null)
            {
                _logger.Log(LogLevel.Error, "Не найден файл настроек.");
                return NotFound("Не найден файл настроек.");
            }

            _dm.SetPath(settings!.Path);

            bool result = _dm.Exist(filename, level, format);

            if (result)
            {
                _logger.Log(LogLevel.Information, "Указанный файл существует!");
                return Ok("Указанный файл существует!");
            }
            else
            {
                _logger.Log(LogLevel.Warning, "Указанный файл не существует!");
                return Ok("Указанный файл не существует!");
            }
        }
    }
}
