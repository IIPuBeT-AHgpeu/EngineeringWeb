namespace EngineeringWeb.Services
{
    public interface ISimpleLogger
    {
        public void Log(LogLevel level, string message);
    }
}
