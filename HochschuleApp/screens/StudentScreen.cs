using HochschuleApp.entity;
using HochschuleApp.service;

namespace HochschuleApp.screens
{
    /// <summary>
    /// Bietet Menüoptionen zum Verwalten von Studenten.
    /// </summary>
    public class StudentScreen : IHochschuleScreen
    {
        private static int choice;
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;
        private readonly SemesterService _semesterService;

        public StudentScreen(StudentService studentService, CourseService courseService, SemesterService semesterService) =>
            (_studentService, _courseService, _semesterService) = (studentService, courseService, semesterService);

        /// <summary>
        /// Startet das Hauptmenü für die Studentenverwaltung.
        /// </summary>
        /// <returns>Die gewählte Menüoption (1-8, wobei 8 das Beenden bedeutet).</returns>
        public int Run()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("\n--- Student Management Menu ---");
                Console.WriteLine("1. Create Student");
                Console.WriteLine("2. Update Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Display All Students");
                Console.WriteLine("5. Display Student by ID");
                Console.WriteLine("6. Enroll Student in Course");
                Console.WriteLine("8. Back to Main Menu");
                Console.WriteLine("----------------------------");
                Console.Write("Enter your choice: ");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < IHochschuleScreen.INITIAL_MENU_POINT || choice > IHochschuleScreen.QUITT_MAIN_MENU)
                {
                    Console.WriteLine($"Invalid choice. Please enter a number between {IHochschuleScreen.INITIAL_MENU_POINT} and {IHochschuleScreen.QUITT_MAIN_MENU}.");
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
                        AddStudentInCourse();
                        break;
                    case 7:
                        choice = IHochschuleScreen.QUITT_MAIN_MENU;
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
        /// Fügt einen existierenden Studenten zu einem bestehenden Kurs hinzu.
        /// </summary>
        private void AddStudentInCourse()
        {
            Console.WriteLine("--- Add Student to Course ---");

            int studentId = InputScreen.GetIntInput($"Enter the ID of the Student to add Course");

            Student student = _studentService.FindByID(studentId);

            // Allow user to select Course for the Student
            int courseId = InputScreen.GetIntInput($"Enter the ID of the Course to add");

            _studentService.AddStudentToCourse(student.Id, courseId);
        }

        /// <summary>
        /// Erstellt einen neuen Studenten.
        /// </summary>
        public void Create()
        {
            Console.WriteLine("--- Create Student ---");

            string surname = InputScreen.GetStringInput("Enter Student Surname");
            string name = InputScreen.GetStringInput("Enter Student Name");
            string address = InputScreen.GetStringInput("Enter Student Address");

            DateTime? birthdate = InputScreen.GetDateTimeInput($"Enter Birthdate ({InputScreen.DateFormat}) (or press Enter to skip)");

            int semesterId = InputScreen.GetIntInput($"Enter ID of the Semester");
            Semester semester = _semesterService.FindByID(semesterId);

            // Allow user to select Courses for the Student
            var courses = InputScreen.GetStringInputs(AvailableCourses(),
                "Select Courses for this Student (enter Course IDs separated by commas, or press Enter to skip)");


            Student newStudent = new()
            {
                Surname = surname,
                Name = name,
                Address = address,
                Birthdate = birthdate,
                Semester = semester,
                Courses = courses
            };

            _studentService.CreateNew(newStudent);
        }

        /// <summary>
        /// Aktualisiert einen existierenden Studenten.
        /// </summary>
        public void Update()
        {
            Console.WriteLine("--- Update Student ---");

            int studentId = InputScreen.GetIntInput($"Enter the ID of the Student to update");

            Student student = _studentService.FindByID(studentId);

            string surname = InputScreen.GetStringInputWithDefaultValue(
                InputScreen.GetStringInput($"Enter Student Surname (or press Enter to keep current) ({student.Surname})"),
                student.Surname);

            string name = InputScreen.GetStringInputWithDefaultValue(
                InputScreen.GetStringInput($"Enter Student Name (or press Enter to keep current) ({student.Name})"),
                student.Name);

            string address = InputScreen.GetStringInputWithDefaultValue(
                InputScreen.GetStringInput($"Enter Student Address (or press Enter to keep current) ({student.Address})"),
                student.Address);

            DateTime? birthdate = InputScreen.GetDateTimeInputWithDefaultValue(
                "$Enter Birthdate ({InputScreen.DateFormat}) (or press Enter to keep current)",
                student.Birthdate);


            int semesterId = InputScreen.GetIntInputWithDefaultValue(
                $"Enter ID of the Semester (or press Enter to keep current)",
                student.Semester?.Id ?? 0);

            if(semesterId != student.Semester?.Id)
            {
                Semester semester = _semesterService.FindByID(semesterId);
                student.Semester = semester;
            }

            // Allow user to select Courses for the Student
            var courses = InputScreen.GetStringInputs(AvailableCourses(),
                "Select Courses for this Student (enter Course IDs separated by commas, or press Enter to skip)");

            Student newStudent = student.CloneObject();

            newStudent.Surname = surname;
            newStudent.Name = name;
            newStudent.Address = address;
            newStudent.Birthdate = birthdate;
            newStudent.Courses = courses;

            _studentService.Update(newStudent.Id, newStudent);
        }

        /// <summary>
        /// Löscht einen Studenten anhand der ID.
        /// </summary>
        public void Delete()
        {
            Console.WriteLine("--- Delete Student by ID ---");

            int studentId = InputScreen.GetIntInput($"Enter the ID of the Student to delete");

            _studentService.DeleteByID(studentId);
        }

        /// <summary>
        /// Zeigt alle Studenten an.
        /// </summary>
        public void DisplayAll()
        {
            Console.WriteLine("--- Display All Students ---");

            var students = _studentService.ListAll();

            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                foreach (var student in students)
                {
                    Display(student);
                }
            }
        }

        /// <summary>
        /// Fordert den Benutzer zur Eingabe einer Studenten-ID auf und zeigt anschließend die Details des Studenten an.
        /// </summary>
        public void Display()
        {
            Console.WriteLine("--- Display Student by ID ---");

            int studentId = InputScreen.GetIntInput($"Enter the ID of the Student to display");

            Student student = _studentService.FindByID(studentId);

            Display(student);
        }

        /// <summary>
        /// Zeigt die Details eines Studenten an, inklusive Name, Vorname, Adresse, Geburtsdatum, Semester und eingeschriebene Kurse.
        /// </summary>
        /// <param name="student">Der Student, dessen Informationen angezeigt werden sollen.</param>
        private static void Display(Student student)
        {
            Console.WriteLine(student.ToString());
            Console.WriteLine("Courses enrolled in");
            foreach (var course in student.Courses)
            {
                Console.WriteLine($" - {course.Id} {course.Name}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Gibt eine Liste aller verfügbaren Kurse zurück.
        /// </summary>
        /// <returns>Eine Liste aller Kurse.</returns>
        private List<Course> AvailableCourses()
        {
            return _courseService.ListAll();
        }
    }
}