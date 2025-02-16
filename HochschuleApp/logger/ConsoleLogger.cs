using System;
namespace HochschuleApp.logger
{
    public static class ConsoleLogger
    {
        public static void LogError(Exception exception)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [Error] {nameof(exception)} - {exception.Message}");
        }

        public static void LogInformation(string message)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [Information] {message}");
        }
    }
}
