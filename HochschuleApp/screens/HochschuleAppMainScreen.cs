using System;
namespace HochschuleApp.screens
{
    /// <summary>
    /// Stellt den Hauptbildschirm der Hochschule-Anwendung dar und bietet ein Menü, in dem Benutzer zwischen Funktionen zur Verwaltung von Kursen, Studenten, Semestern und Dozenten navigieren können.
    /// </summary>
    public class HochschuleAppMainScreen
    {
        private readonly CourseScreen _courseScreen;
        private readonly StudentScreen _studentScreen;
        private readonly SemesterScreen _semesterScreen;
        private readonly LecturerScreen _lecturerScreen;
        private static int userInputNumber;

        /// <summary>
        /// Initialisiert eine neue Instanz der HochschuleAppMainScreen-Klasse mit Referenzen auf die Bildschirme für die Verwaltung von Kursen, Studenten, Semestern und Dozenten.
        /// </summary>
        /// <param name="courseScreen">Die CourseScreen-Instanz zur Verwaltung von Kursen.</param>
        /// <param name="studentScreen">Die StudentScreen-Instanz zur Verwaltung von Studenten.</param>
        /// <param name="semesterScreen">Die SemesterScreen-Instanz zur Verwaltung von Semestern.</param>
        /// <param name="lecturerScreen">Die LecturerScreen-Instanz zur Verwaltung von Dozenten.</param>
        public HochschuleAppMainScreen(CourseScreen courseScreen, StudentScreen studentScreen, SemesterScreen semesterScreen, LecturerScreen lecturerScreen) =>
            (_courseScreen, _studentScreen, _semesterScreen, _lecturerScreen) = (courseScreen, studentScreen, semesterScreen, lecturerScreen);

        /// <summary>
        /// Startet das Hauptmenü der Hochschule-Anwendung und ermöglicht Benutzern die Navigation zwischen verschiedenen Verwaltungsfunktionen, bis sie die Anwendung beenden.
        /// </summary>
        public void Run()
        {
            while (userInputNumber != IHochschuleScreen.QUITT_MAIN_MENU)
            {
                // Screen Main Menue 
                Console.Clear();
                Console.WriteLine("***************************************");
                Console.WriteLine("Main Menue");
                Console.WriteLine("***************************************");
                Console.WriteLine("1 Course Management");
                Console.WriteLine("2 Semester Management");
                Console.WriteLine("3 Lecturer Management");
                Console.WriteLine("4 Student Management");
                Console.WriteLine("8 Quit Program");
                Console.WriteLine("***************************************");
                Console.Write("Please select menue item and hit enter: ");

                userInputNumber = InputScreen.GetIntInput("Menu point");

                switch (userInputNumber)
                {
                    // Screen Course Management
                    case 1:
                        while (userInputNumber != IHochschuleScreen.QUITT_MAIN_MENU)
                        {
                            userInputNumber = _courseScreen.Run();
                        }
                        userInputNumber = -1;
                        break;

                    // Screen Semester Management
                    case 2:
                        while (userInputNumber != IHochschuleScreen.QUITT_MAIN_MENU)
                        {
                            userInputNumber = _semesterScreen.Run();
                        }
                        userInputNumber = -1;
                        break;

                    // Screen Lecturer Management
                    case 3:
                        while (userInputNumber != IHochschuleScreen.QUITT_MAIN_MENU)
                        {
                            userInputNumber = _lecturerScreen.Run();
                        }
                        userInputNumber = -1;
                        break;
                    // Screen Student Management
                    case 4:
                        while (userInputNumber != IHochschuleScreen.QUITT_MAIN_MENU)
                        {
                            userInputNumber = _studentScreen.Run();
                        }
                        userInputNumber = -1;
                        break;

                    // Quitt Main Menue
                    case 8:
                        userInputNumber = IHochschuleScreen.QUITT_MAIN_MENU;
                        Console.Clear();
                        break;

                    // Default
                    default:
                        Console.WriteLine("Unfortunately your input cant be read.");
                        break;
                }
            }
        }
    }
}

