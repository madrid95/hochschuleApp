using System;
using HochschuleApp.screens;

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

        /// <summary>
        /// Konstruktor für die Application-Klasse.
        /// </summary>
        /// <param name="hochschuleAppMainScreen">Das Hauptfenster der Anwendung.</param>
        public Application(HochschuleAppMainScreen hochschuleAppMainScreen)
        {
            _hochschuleAppMainScreen = hochschuleAppMainScreen;
            add += new DelEventHandler(Run);  // Beachte: Anonyme Methode wird direkt hinzugefügt
            add.Invoke();
        }

        /// <summary>
        /// Startet die Anwendungsausführung.
        /// </summary>
        /// <remarks>
        /// Diese Methode wird beim Start der Anwendung aufgerufen und delegiert die eigentliche Ausführung an das Hauptfenster (`_hochschuleAppMainScreen.Run()`).
        /// </remarks>
        public void Run()
        {
            _hochschuleAppMainScreen.Run();
        }
    }
}
