using Autofac;
using HochschuleApp.screens;
using Microsoft.Extensions.Configuration;

namespace HochschuleApp
{
    /// <summary>
    /// Einstiegspunkt der Anwendung.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Hauptmethode der Anwendung.
        /// </summary>
        /// <param name="args">Kommandozeilenargumente.</param>
        public static void Main(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Base Directory: {basePath}");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            bool choice = StartScreen.Start();

            var config = ConfigureClass.Configure(configuration, choice);
            using var scope = config.BeginLifetimeScope();
            scope.Resolve<Application>();
        }
    }
}
