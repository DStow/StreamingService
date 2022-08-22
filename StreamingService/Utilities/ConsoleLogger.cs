namespace StreamingService.Utilities
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            System.Console.WriteLine("Log: " + message);
        }
    }
}
