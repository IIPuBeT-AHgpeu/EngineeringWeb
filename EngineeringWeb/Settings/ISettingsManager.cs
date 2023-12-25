namespace EngineeringWeb.Settings
{
    public interface ISettingsManager
    {
        public void CreateSettingsFile();
        public void SetSettings(Settings settings);
        public Settings? GetSettings();
        public void DeleteSettingsFile();
    }
}
