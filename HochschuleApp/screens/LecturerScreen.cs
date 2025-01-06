using HochschuleApp.entity;
using HochschuleApp.service;
using HochschuleApp.exceptions;

namespace HochschuleApp.screens
{
    /// <summary>
    /// Stellt den Bildschirm für die Dozentenverwaltung dar und bietet Funktionen zum Anlegen, Aktualisieren, Löschen und Anzeigen von Dozenten.
    /// </summary>
    public class LecturerScreen : IHochschuleScreen
    {
        private static int choice;

        private readonly CourseService _courseService;
        private readonly LecturerService _lecturerService;

        /// <summary>
        /// Initialisiert eine neue Instanz der LecturerScreen-Klasse mit Referenzen auf die Dienste für die Kurs- und Dozentenverwaltung.
        /// </summary>
        /// <param name="courseService">Der CourseService zum Verwalten von Kursen.</param>
        /// <param name="lecturerService">Der LecturerService zum Verwalten von Dozenten.</param>
        public LecturerScreen(CourseService courseService, LecturerService lecturerService) =>
            (_courseService, _lecturerService) = (courseService, lecturerService);

        /// <summary>
        /// Startet das Hauptmenü des Dozentenverwaltungsbildschirms und ermöglicht die Auswahl verschiedener Funktionen bis zum Beenden des Programms.
        /// </summary>
        /// <returns>Die gewählte Menüoption (entspricht IHochschuleScreen.QUITT_MAIN_MENU zum Beenden).</returns>
        public int Run()
        {
            do
            {
                Console.WriteLine("\n--- Lecturer Management Menu ---");
                Console.WriteLine("1. Create Lecturer");
                Console.WriteLine("2. Update Lecturer");
                Console.WriteLine("3. Delete Lecturer");
                Console.WriteLine("4. Display All Lecturers");
                Console.WriteLine("5. Display Lecturer by ID");
                Console.WriteLine("6. Add Lecturer to Course");
                Console.WriteLine("8. Back to Main Menu");
                Console.WriteLine("9. Clear Console");
                Console.WriteLine("----------------------------");
                Console.Write("Enter your choice: ");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < IHochschuleScreen.INITIAL_MENU_POINT || choice > IHochschuleScreen.CLEAR_SCREEN)
                {
                    Console.WriteLine($"Invalid choice. Please enter a number between {IHochschuleScreen.INITIAL_MENU_POINT} and {IHochschuleScreen.CLEAR_SCREEN}.");
                    Console.Write("Enter your choice: ");
                }

                switch (choice)
                {
                    case 1:
                        Create();
                        break;
                    case 2:
                        Update();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        DisplayAll();
                        break;
                    case 5:
                        Display();
                        break;
                    case 6:
                        AddLecturerToCourse();
                        break;
                    case 8:
                        choice = IHochschuleScreen.QUITT_MAIN_MENU;
                        Console.Clear();
                        break;
                    case 9:
                        Console.Clear();
                        break;
                    // Default case
                    default:
                        Console.WriteLine("Unfortunately, your input cannot be read!");
                        break;
                }

            } while (choice != IHochschuleScreen.QUITT_MAIN_MENU);

            return choice;
        }

        public void AddLecturerToCourse()
        {
            Console.WriteLine("--- Add Lecturer to Course ---");

            int lecturerId = InputScreen.GetIntInput($"Enter the ID of the Lecturer to add Course");

            Lecturer? lecturer = ExceptionHandler.Invoke(() => _lecturerService.FindByID(lecturerId));

            if (lecturer == null)
            {
                return;
            }

            // Allow user to select Lecturer for the Course
            int courseId = InputScreen.GetIntInput($"Enter the ID of the Course to add Lecturer");

            Course? course = ExceptionHandler.Invoke(() => _courseService.FindByID(courseId));

            if (course == null)
            {
                return;
            }

            ExceptionHandler.Invoke(() => _lecturerService.AddLecturerToCourse(lecturer.Id, courseId));
        }

        /// <summary>
        /// Erstellt einen neuen Dozenten.
        /// </summary>
        public void Create()
        {
            Console.WriteLine("--- Create Lecturer ---");

            string surname = InputScreen.GetStringInput("Enter Lecturer Surname");
            string name = InputScreen.GetStringInput("Enter Lecturer Name");
            string address = InputScreen.GetStringInput("Enter Lecturer Address");

            DateTime? birthdate = InputScreen.GetDateTimeInput($"Enter Birthdate ({InputScreen.DateFormat}) (or press Enter to skip)");

            Degree degree = DegreeScreen.GetDegreeFromUser();

            // Allow user to select Courses for the Lecturer
            var courses = InputScreen.GetStringInputs(AvailableCourses(),
                "Select Courses for this Lecturer (enter Course IDs separated by commas, or press Enter to skip)");

            Lecturer newLecturer = new()
            {
                Surname = surname,
                Name = name,
                Address = address,
                Birthdate = birthdate,
                Degree = degree,
                Courses = courses
            };

            ExceptionHandler.Invoke(() => _lecturerService.CreateNew(newLecturer));
        }

        /// <summary>
        /// Aktualisiert einen vorhandenen Dozenten.
        /// </summary>
        public void Update()
        {
            Console.WriteLine("--- Update Lecturer ---");

            int lecturerId = InputScreen.GetIntInput($"Enter the ID of the Lecturer to update");

            Lecturer? lecturer = ExceptionHandler.Invoke(() => _lecturerService.FindByID(lecturerId));

            if (lecturer != null)
            {
                string surname = InputScreen.GetStringInputWithDefaultValue(
                InputScreen.GetStringInput($"Enter Lecturer Surname (or press Enter to keep current) ({lecturer.Surname})"),
                lecturer.Surname);

                string name = InputScreen.GetStringInputWithDefaultValue(
                    InputScreen.GetStringInput($"Enter Lecturer Name (or press Enter to keep current) ({lecturer.Name})"),
                    lecturer.Name);

                string address = InputScreen.GetStringInputWithDefaultValue(
                    InputScreen.GetStringInput($"Enter Lecturer Address (or press Enter to keep current) ({lecturer.Address})"),
                    lecturer.Address);

                DateTime? birthdate = InputScreen.GetDateTimeInputWithDefaultValue(
                    $"Enter Birthdate ({InputScreen.DateFormat}) (or press Enter to keep current)",
                    lecturer.Birthdate);

                Degree degree = DegreeScreen.GetDegreeFromUserWithDefaultValue(lecturer.Degree);

                // Allow user to select Courses for the Lecturer
                var courses = InputScreen.GetStringInputs(AvailableCourses(),
                    "Select Courses for this Lecturer (enter Course IDs separated by commas, or press Enter to skip)");

                Lecturer newLecturer = lecturer.CloneObject();

                newLecturer.Surname = surname;
                newLecturer.Name = name;
                newLecturer.Address = address;
                newLecturer.Birthdate = birthdate;
                newLecturer.Degree = degree;
                newLecturer.Courses = courses;

                ExceptionHandler.Invoke(() => _lecturerService.Update(newLecturer.Id, newLecturer));
            }
        }

        /// <summary>
        /// Löscht einen Dozenten anhand seiner ID.
        /// </summary>
        public void Delete()
        {
            Console.WriteLine("--- Delete Lecturer by ID ---");

            int lecturerId = InputScreen.GetIntInput($"Enter the ID of the Lecturer to delete");

            ExceptionHandler.Invoke(() => _lecturerService.DeleteByID(lecturerId));
        }

        /// <summary>
        /// Zeigt detaillierte Informationen zu einem Dozenten anhand seiner ID an.
        /// </summary>
        public void Display()
        {
            Console.WriteLine("--- Display Lecturer by ID ---");

            int lecturerId = InputScreen.GetIntInput($"Enter the ID of the Lecturer to display");

            var lecturer = ExceptionHandler.Invoke(() => _lecturerService.FindByID(lecturerId));

            if (lecturer == null)
            {
                return;
            }

            Display(lecturer);
        }

        /// <summary>
        /// Zeigt alle Dozenten an.
        /// </summary>
        public void DisplayAll()
        {
            Console.WriteLine("--- Display All Lecturers ---");

            List<Lecturer> lecturers = ExceptionHandler.Invoke( _lecturerService.ListAll);

            if (lecturers.Count == 0)
            {
                Console.WriteLine("There is no Lecture.");
                return;
            }

            foreach (var lecturer in lecturers)
            {
                Display(lecturer);
            }
        }

        /// <summary>
        /// Zeigt detaillierte Informationen zu einem Dozenten an.
        /// </summary>
        /// <param name="lecturer">Der Dozent, dessen Informationen angezeigt werden sollen.</param>
        private static void Display(Lecturer lecturer)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(lecturer.ToString());

            Console.WriteLine("\nCourses associated with Lecturer:");

            if (lecturer.Courses.Count == 0)
            {
                Console.WriteLine("There is no Course offered by this Student.");
            }

            foreach (var course in lecturer.Courses)
            {
                Console.WriteLine(course.ToShortString());
            }

            Console.WriteLine("-----------------------------------------------");
        }

        /// <summary>
        /// Gibt eine Liste aller verfügbaren Kurse zurück.
        /// </summary>
        /// <returns>Eine Liste aller Kurse.</returns>
        private List<Course> AvailableCourses()
        {
            return ExceptionHandler.Invoke(_courseService.ListAll);
        }
    }
}
