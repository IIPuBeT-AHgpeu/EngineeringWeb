namespace EngineeringWeb.Services
{
    public class SimpleLogger : ISimpleLogger
    {
        public void Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Information:
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("info");
                        break;
                    }
                case LogLevel.Warning:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("warn");
                        break;
                    }
                case LogLevel.Error:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("err");
                        break;
                    }
                default:
                    {
                        Console.Write("other");
                        break;
                    }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" " + DateTime.Now.ToString() + ": " + message + "\n");
        }
    }
}
