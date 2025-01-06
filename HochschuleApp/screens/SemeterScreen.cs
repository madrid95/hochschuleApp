using HochschuleApp.entity;
using HochschuleApp.service;
using HochschuleApp.exceptions;

namespace HochschuleApp.screens
{
    /// <summary>
    /// Bietet Menüoptionen zum Verwalten von Semestern. Ermöglicht das Erstellen, Aktualisieren, Löschen, Anzeigen und Bearbeiten von Semesterdaten.
    /// </summary>
    public class SemesterScreen : IHochschuleScreen
    {
        private static int choice;
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;
        private readonly SemesterService _semesterService;

        public SemesterScreen(StudentService studentService, CourseService courseService, SemesterService semesterService) =>
            (_studentService, _courseService, _semesterService) = (studentService, courseService, semesterService);

        /// <summary>
        /// Startet das Hauptmenü für die Semesterverwaltung.
        /// </summary>
        /// <returns>Die gewählte Menüoption (1-8, wobei 8 das Beenden bedeutet).</returns>
        public int Run()
        {
            do
            {
                Console.WriteLine("\n--- Semester Management Menu ---");
                Console.WriteLine("1. Create Semester");
                Console.WriteLine("2. Update Semester");
                Console.WriteLine("3. Delete Semester");
                Console.WriteLine("4. Display All Semesters");
                Console.WriteLine("5. Display Semester by ID");
                Console.WriteLine("6. Add Course to Semester");
                Console.WriteLine("7. Add Student to Semester");
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
                        AddCourseToSemester();
                        break;
                    case 7:
                        AddStudentToSemester();
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

        /// <summary>
        /// Erstellt ein neues Semester.
        /// </summary>
        public void Create()
        {
            Console.WriteLine("--- Create Semester ---");

            string name = InputScreen.GetStringInput("Enter Semester Name");

            DateTime? startDate = InputScreen.GetDateTimeInput($"Enter Start Date ({InputScreen.DateFormat}) (or press Enter to skip)");

            DateTime? endDate = InputScreen.GetDateTimeInput($"Enter End Date ({InputScreen.DateFormat}) (or press Enter to skip)");

            // Allow user to select Students for the Semester
            var students = InputScreen.GetStringInputs(_studentService.ListAll(),
                "Select Students for this Semester (enter Student IDs separated by commas, or press Enter to skip)");

            // Allow user to select Courses for the Semester
            var courses = InputScreen.GetStringInputs(_courseService.ListAll(),
                "Select Courses for this Semester (enter Course IDs separated by commas, or press Enter to skip)");

            Semester newSemester = new()
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Courses = courses,
                Students = students
            };

            //Create new Semester
            ExceptionHandler.Invoke(() => _semesterService.CreateNew(newSemester));
        }

        /// <summary>
        /// Löscht ein existierendes Semester anhand der ID.
        /// </summary>
        public void Delete()
        {
            Console.WriteLine("--- Delete Semester by ID ---");

            int semesterId = InputScreen.GetIntInput($"Enter the ID of the Semester to delete");

            //Delete Semester by Id
            ExceptionHandler.Invoke(() => _semesterService.DeleteByID(semesterId));
        }

        /// <summary>
        /// Aktualisiert die Daten eines existierenden Semesters.
        /// </summary>
        public void Update()
        {
            Console.WriteLine("--- Update Semester ---");

            int semesterId = InputScreen.GetIntInput($"Enter the ID of the Semester to update");

            Semester? semester = ExceptionHandler.Invoke(() => _semesterService.FindByID(semesterId));

            if (semester == null)
            {
                return;
            }

            //Allow user to update name or keep the current value
            string name = InputScreen.GetStringInputWithDefaultValue(
                InputScreen.GetStringInput($"Enter Semester Name (or press Enter to keep current) ({semester.Name})"),
                semester.Name);

            DateTime? startDate = InputScreen.GetDateTimeInputWithDefaultValue(
                $"Enter Start Date ({InputScreen.DateFormat}) (or press Enter to skip)",
                semester.StartDate);

            DateTime? endDate = InputScreen.GetDateTimeInputWithDefaultValue(
                $"Enter End Date ({InputScreen.DateFormat}) (or press Enter to skip)",
                semester.EndDate);

            // Allow user to select Students for the Semester
            var students = InputScreen.GetStringInputs(AvailableStudents(),
                "Select Students for this Semester (enter Student IDs separated by commas, or press Enter to skip)");

            // Allow user to select Courses for the Semester
            var courses = InputScreen.GetStringInputs(AvailableCourses(),
                "Select Courses for this Semester (enter Course IDs separated by commas, or press Enter to skip)");

            Semester newSemester = semester.CloneObject();

            newSemester.Name = name;
            newSemester.StartDate = startDate;
            newSemester.EndDate = endDate;
            newSemester.Courses = courses;
            newSemester.Students = students;
            newSemester.Courses = courses;

            //Update Semester by ID
            ExceptionHandler.Invoke(() => _semesterService.Update(semesterId, newSemester));
        }

        /// <summary>
        /// Fügt einen Studenten zu einem existierenden Semester hinzu.
        /// </summary>
        public void AddStudentToSemester()
        {
            Console.WriteLine("--- Add Student to Semester ---");

            int semesterId = InputScreen.GetIntInput($"Enter the ID of the Semester to add Student");

            Semester? semester = ExceptionHandler.Invoke(() => _semesterService.FindByID(semesterId));

            if (semester == null)
            {
                return;
            }

            // Allow user to select Student for the Semester
            int studentId = InputScreen.GetIntInput($"Enter the ID of the Student to add");

            // Add Student to Semester
            ExceptionHandler.Invoke(() => _semesterService.AddStudentToSemester(semester.Id, studentId));

        }

        /// <summary>
        /// Fügt einen Kurs zu einem existierenden Semester hinzu.
        /// </summary>
        public void AddCourseToSemester()
        {
            Console.WriteLine("--- Add Course to Semester ---");

            int semesterId = InputScreen.GetIntInput($"Enter the ID of the Semester to add Course");

            Semester? semester = ExceptionHandler.Invoke(() => _semesterService.FindByID(semesterId));

            if (semester == null)
            {
                return;
            }

            // Allow user to select Course for the Semester
            int courseId = InputScreen.GetIntInput($"Enter the ID of the Course to add");

            // Add Course to Semester
            ExceptionHandler.Invoke(() => _semesterService.AddCourseToSemester(semester.Id, courseId));

        }

        /// <summary>
        /// Zeigt alle existierenden Semester an.
        /// </summary>
        public void DisplayAll()
        {
            Console.WriteLine("--- Display All Semesters ---");

            var semesters = ExceptionHandler.Invoke(_semesterService.ListAll);

            if (semesters.Count == 0)
            {
                Console.WriteLine("No semesters found.");
                return;
            }

            foreach (var semester in semesters)
            {
                Display(semester);
            }
        }

        /// <summary>
        /// Zeigt die Details eines Semesters anhand der ID an.
        /// </summary>
        public void Display()
        {
            Console.WriteLine("--- Display Semester by ID ---");

            int semesterId = InputScreen.GetIntInput($"Enter the ID of the Semester to display");

            Semester? semester = ExceptionHandler.Invoke(() => _semesterService.FindByID(semesterId));

            if (semester == null)
            {
                return;
            }

            Display(semester);
        }

        /// <summary>
        /// Zeigt die Details eines Semesters an.
        /// </summary>
        /// <param name="semester">Das Semester, dessen Details angezeigt werden sollen.</param>
        private static void Display(Semester semester)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(semester.ToString());

            Console.WriteLine("\nStudents registered in this Semester:");

            if (semester.Students.Count == 0)
            {
                Console.WriteLine("There is no Student registered in this Semester.");
            }

            foreach (var student in semester.Students)
            {
                Console.WriteLine(student.ToShortString());
            }

            Console.WriteLine("\nSemesters associated with Courses:");

            if (semester.Courses.Count == 0)
            {
                Console.WriteLine("There is no Course added in this Semester.");
            }

            foreach (var course in semester.Courses)
            {
                Console.WriteLine(course.ToShortString());
            }

            Console.WriteLine("-----------------------------------------------");
        }

        // <summary>
        /// Gibt eine Liste aller verfügbaren Kurse zurück.
        /// </summary>
        /// <returns>Eine Liste aller Kurse.</returns>
        private List<Course> AvailableCourses()
        {
            return ExceptionHandler.Invoke(_courseService.ListAll);
        }

        /// <summary>
        /// Gibt eine Liste aller verfügbaren Studenten zurück.
        /// </summary>
        /// <returns>Eine Liste aller Studenten.</returns>
        private List<Student> AvailableStudents()
        {
            return ExceptionHandler.Invoke(_studentService.ListAll);
        }
    }
}
