using HochschuleApp.entity;
using HochschuleApp.service;

namespace HochschuleApp.screens
{
    public class CourseScreen : IHochschuleScreen
    {
        private static int choice;

        private readonly StudentService _studentService;
        private readonly CourseService _courseService;
        private readonly SemesterService _semesterService;
        private readonly LecturerService _lecturerService;

        public CourseScreen(StudentService studentService, CourseService courseService, SemesterService semesterService, LecturerService lecturerService) =>
            (_studentService, _courseService, _semesterService, _lecturerService) = (studentService, courseService, semesterService, lecturerService);

        /// <summary>
        /// Startet das Hauptmenü für die Kursverwaltung und führt die vom Benutzer ausgewählte Aktion aus.
        /// </summary>
        /// <returns>Die gewählte Menüoption (1-8, wobei 8 das Beenden bedeutet).</returns>
        public int Run()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("\n--- Course Management Menu ---");
                Console.WriteLine("1. Create Course");
                Console.WriteLine("2. Update Course");
                Console.WriteLine("3. Delete Course");
                Console.WriteLine("4. Display All Courses");
                Console.WriteLine("5. Display Course by ID");
                Console.WriteLine("6. Add Student to Courses");
                Console.WriteLine("7. Add Lecturer to Courses");
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
                        AddStudentToCourse();
                        break;
                    case 7:
                        AddLecturerToCourse();
                        break;
                    case 8:
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
        /// Performs Delete operation on a Course.
        /// </summary>
        public void Delete()
        {
            Console.WriteLine("--- Delete Course by ID ---");

            int courseId = InputScreen.GetIntInput($"Enter the ID of the Course to delete");

            _courseService.DeleteByID(courseId);
        }

        /// <summary>
        /// Performs Update operation on a Course.
        /// </summary>
        public void Update()
        {
            Console.WriteLine("--- Update Course ---");

            int courseId = InputScreen.GetIntInput($"Enter the ID of the Course to update");

            Course course = _courseService.FindByID(courseId);

            //Allow user to update name or keep the current value
            string name = InputScreen.GetStringInputWithDefaultValue(
                InputScreen.GetStringInput($"Enter Semester Name (or press Enter to keep current) ({course.Name}):"),
                course.Name);

            string description = InputScreen.GetStringInputWithDefaultValue(
                InputScreen.GetStringInput($"Enter Semester Name (or press Enter to keep current) ({course.Name}):"),
                course.Name);

            // Allow user to select Students for the Course
            var students = InputScreen.GetStringInputs(AvailableStudents(),
                "Select Students for this Course (enter Student IDs separated by commas, or press Enter to skip)");

            // Allow user to select Courses for the Semester
            var semesters = InputScreen.GetStringInputs(AvailableSemesters(),
                "Select Semesters for this Course (enter Course IDs separated by commas, or press Enter to skip)");

            Course newCourse = course.CloneObject();

            newCourse.Name = name;
            newCourse.Description = description;
            newCourse.Students = students;
            newCourse.Semesters = semesters;

            _courseService.Update(newCourse.Id, newCourse);
        }

        /// <summary>
        /// Displays all existing Courses.
        /// </summary>
        public void DisplayAll()
        {
            Console.WriteLine("--- Display All Courses ---");

            var courses = _courseService.ListAll();

            foreach(var course in courses)
            {
                Display(course);
            }
        }

        /// <summary>
        /// Displays the details of a Course by its ID.
        /// </summary>
        public void Display()
        {
            Console.WriteLine("--- Display Course by ID ---");

            int courseId = InputScreen.GetIntInput($"Enter the ID of the Course to display");

            Course course = _courseService.FindByID(courseId);

            Display(course);
        }

        /// <summary>
        /// Adds a Student to an existing Course.
        /// </summary>
        public void AddStudentToCourse()
        {
            Console.WriteLine("--- Add Student to Course ---");

            int courseId = InputScreen.GetIntInput($"Enter the ID of the Course to add Student");

            Course course = _courseService.FindByID(courseId);

            // Allow user to select Student for the Course
            int studentId = InputScreen.GetIntInput($"Enter the ID of the Student to add");

            _courseService.AddStudentToCourse(course.Id, studentId);
        }

        /// <summary>
        /// Adds a Lecturer to an existing Course.
        /// </summary>
        public void AddLecturerToCourse()
        {
            Console.WriteLine("--- Add Lecturer to Course ---");

            int courseId = InputScreen.GetIntInput($"Enter the ID of the Course to add Lecturer");

            Course course = _courseService.FindByID(courseId);

            // Allow user to select Lecturer for the Course
            int lecturerId = InputScreen.GetIntInput($"Enter the ID of the Lecturer to add");

            _courseService.AddLecturerToCourse(course.Id, lecturerId);
        }

        /// <summary>
        /// Performs Create operation on a Course.
        /// </summary>
        public void Create()
        {
            Console.WriteLine("--- Create Course ---");

            string name = InputScreen.GetStringInput("Enter Course Name");
            string description = InputScreen.GetStringInput("Enter Course Description");

            int lecturerId = InputScreen.GetIntInput("Enter Lecturer ID (or press Enter to skip)");
            Lecturer lecturer = _lecturerService.FindByID(lecturerId);

            DateTime? startDate = InputScreen.GetDateTimeInput($"Enter Start Date ({InputScreen.DateFormat}) (or press Enter to skip)");

            DateTime? endDate = InputScreen.GetDateTimeInput($"Enter End Date ({InputScreen.DateFormat}) (or press Enter to skip)");

            // Allow user to select Students for the Course
            var students = InputScreen.GetStringInputs(AvailableStudents(),
                "Select Students for this Course (enter Student IDs separated by commas, or press Enter to skip)");

            // Allow user to select Semesters for the Course
            var semesters = InputScreen.GetStringInputs(AvailableSemesters(),
                "Select Semesters for this Course (enter Semester IDs separated by commas, or press Enter to skip)");

            Course newCourse = new()
            {
                Name = name,
                Description = description,
                Lecturer = lecturer,
                Startdate = startDate,
                Enddate = endDate,
                Students = students,
                Semesters = semesters
            };

            _courseService.CreateNew(newCourse);
        }

        /// <summary>
        /// Displays the details of a Course object.
        /// </summary>
        /// <param name="course">The Course object whose details will be displayed.</param>
        private static void Display(Course course)
        {
            Console.WriteLine(course.ToString());

            Console.WriteLine("\nStudents enrolled in this Course:");
            foreach (var student in course.Students)
            {
                Console.WriteLine($" - {student.Id} {student.Name} {student.Surname}");
            }

            Console.WriteLine("\nSemesters associated with this Course:");
            foreach (var semester in course.Semesters)
            {
                Console.WriteLine($" - {semester.Id} {semester.Name}");
            }
        }

        /// <summary>
        /// Gibt eine Liste aller verfügbaren Studenten zurück.
        /// </summary>
        /// <returns>Eine Liste aller Studenten.</returns>
        private List<Student> AvailableStudents()
        {
            return _studentService.ListAll();
        }

        /// <summary>
        /// Gibt eine Liste aller verfügbaren Semester zurück.
        /// </summary>
        /// <returns>Eine Liste aller Semester.</returns>
        private List<Semester> AvailableSemesters()
        {
            return _semesterService.ListAll();
        }
    }
}

