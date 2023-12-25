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
                _logger.Log(LogLevel.Error, "�� ������� �������� ���������� � ���������� ������.");
                return null;
            }
            else 
            {
                _logger.Log(LogLevel.Information, "���� �������� ��������� ������.");
                return result.ModelSettings;
            }
        }

        [HttpGet("create")]
        public IActionResult CreateModel()
        {
            bool isCreated = model.CreateModel();

            if (!isCreated)
            {
                _logger.Log(LogLevel.Error, "�� ������� ������� ������.");
                return StatusCode(404, "�� ������� ������� ������.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "������ ���� �������.");
                return Ok();
            }
        }

        [HttpDelete]
        public IActionResult DeleteModel()
        {
            bool isCreated = model.DeleteModel();

            if (!isCreated)
            {
                _logger.Log(LogLevel.Error, "�� ������� ������� ������.");
                return StatusCode(404, "�� ������� ������� ������.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "������ ���� �������.");
                return Ok();
            }
        }

        [HttpPost("check")]
        public IActionResult Check(IFormFile file)
        {
            var settings = _manager.GetSettings();
            if (settings == null)
            {
                _logger.Log(LogLevel.Error, "���� �������� �� ������.");
                return BadRequest("��� ����� ��������.");
            }
            if (!model.IsModelExist(settings.Path + "\\" + settings.ModelName))
            {
                _logger.Log(LogLevel.Error, "�� ������� ����� ���� ������.");
                return BadRequest("��� ������.");
            }
            if (file == null)
            {
                _logger.Log(LogLevel.Error, "�������� �� ���� ��������.");
                return BadRequest("��� ��������.");
            }
            var result = model.UseModel(file);

            _logger.Log(LogLevel.Information, "������ ������ ���������: " + result);
            return Ok(result);
        }
    }
}
