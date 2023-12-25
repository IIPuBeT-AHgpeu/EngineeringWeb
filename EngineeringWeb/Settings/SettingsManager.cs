using System.Reflection;
using System.Text.Json;

namespace EngineeringWeb.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private string _settingsPath;
        private Settings _default;
        private string _fullFileName; 
        public SettingsManager(string appPath)
        {
            _settingsPath = appPath;
            _default = new Settings()
            {
                Path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                ModelName = "model.obj",
                ModelSettings = new ModelSettings() { EpochCount = 5, LayersCount = 3 }
            };
            _fullFileName = _settingsPath + "\\settings.json";

            CreateSettingsFile();
        }
        public void CreateSettingsFile()
        {
            if (!File.Exists(_fullFileName))
            {
                if (!Directory.Exists(_settingsPath)) Directory.CreateDirectory(_settingsPath);

                File.WriteAllText(_fullFileName, JsonSerializer.Serialize<Settings>(_default));
            }
        }
        public void DeleteSettingsFile()
        {
            if (File.Exists(_fullFileName)) File.Delete(_fullFileName);
        }
        public Settings? GetSettings()
        {
            try
            {
                if (File.Exists(_fullFileName)) return JsonSerializer.Deserialize<Settings>(File.ReadAllText(_fullFileName));
                else return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void SetSettings(Settings settings)
        {
            try
            {
                if (File.Exists(_fullFileName)) File.WriteAllText(_fullFileName, JsonSerializer.Serialize<Settings>(settings));
                else throw new Exception("Файл настроек не найден.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
