using System;

namespace HochschuleApp.screens
{
    /// <summary>
    /// Schnittstelle für Hochschule-Screens.
    /// </summary>
    /// <remarks>
    /// Definiert die grundlegenden Methoden für alle Screens innerhalb der Hochschule-Anwendung.
    /// </remarks>
    public interface IHochschuleScreen
    {
        /// <summary>
        /// Konstante für die Menüoption "Zurück zum Hauptmenü".
        /// </summary>
        public const int QUITT_MAIN_MENU = 8;

        /// <summary>
        /// Konstante für den ersten Menüpunkt.
        /// </summary>
        public const int INITIAL_MENU_POINT = 1;

        /// <summary>
        /// Konstante für die Menüoption "Lösche Konsole".
        /// </summary>
        public const int CLEAR_SCREEN = 9;

        /// <summary>
        /// Erstellt einen neuen Datensatz.
        /// </summary>
        void Create();

        /// <summary>
        /// Löscht einen Datensatz.
        /// </summary>
        void Delete();

        /// <summary>
        /// Aktualisiert einen Datensatz.
        /// </summary>
        void Update();

        /// <summary>
        /// Zeigt alle Datensätze an.
        /// </summary>
        void DisplayAll();

        /// <summary>
        /// Zeigt Details eines bestimmten Datensatzes an.
        /// </summary>
        void Display();

        /// <summary>
        /// Führt die Aktionen des Screens aus und gibt den ausgewählten Menüpunkt zurück.
        /// </summary>
        /// <returns>Die ID des ausgewählten Menüpunkts.</returns>
        int Run();
    }
}
