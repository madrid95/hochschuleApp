using System;
using HochschuleApp.screens;
using HochschuleApp.data.migration;

namespace HochschuleApp
{
    /// <summary>
    /// Hauptanwendungsklasse für die Hochschule-App.
    /// </summary>
    /// <remarks>
    /// Diese Klasse ist der Einstiegspunkt für die Anwendung. Sie initialisiert das Hauptfenster (`HochschuleAppMainScreen`) und startet die Anwendungsausführung.
    /// </remarks>
    public class Application : IApplication
    {
        /// <summary>
        /// Delegatentyp für Ereignishandler, die beim Start der Anwendung aufgerufen werden.
        /// </summary>
        public delegate void DelEventHandler();

        /// <summary>
        /// Ereignis, das beim Start der Anwendung ausgelöst wird.
        /// </summary>
        public event DelEventHandler add;

        private readonly HochschuleAppMainScreen _hochschuleAppMainScreen;
        private readonly DataSeeder _dataSeeder;

        /// <summary>
        /// Konstruktor für die Application-Klasse.
        /// </summary>
        /// <param name="hochschuleAppMainScreen">Das Hauptfenster der Anwendung.</param>
        public Application(HochschuleAppMainScreen hochschuleAppMainScreen, DataSeeder dataSeeder)
        {
            _dataSeeder = dataSeeder;
            _hochschuleAppMainScreen = hochschuleAppMainScreen;
            add += new DelEventHandler(Run);
            add.Invoke();
        }

        /// <summary>
        /// Startet die Anwendungsausführung.
        /// </summary>
        /// <remarks>
        /// Diese Methode wird beim Start der Anwendung aufgerufen und delegiert die eigentliche Ausführung an das Hauptfenster (`_hochschuleAppMainScreen.Run()`).
        /// </remarks>
        public async void Run()
        {

            // Seed the database on application startup
            Task.Run(_dataSeeder.SeedAsync);

            _hochschuleAppMainScreen.Run();
        }
    }
}
