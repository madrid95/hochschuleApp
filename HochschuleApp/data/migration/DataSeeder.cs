using HochschuleApp.context;
using HochschuleApp.entity;

namespace HochschuleApp.data.migration
{
    public class DataSeeder
    {
        private readonly HochschuleContext _hochschuleContext;
        private readonly bool _inMemoryDatabase;

        public DataSeeder(HochschuleContext hochschuleContext, bool inMemoryDatabse) =>
        (_hochschuleContext, _inMemoryDatabase) = (hochschuleContext, inMemoryDatabse);

        public async Task SeedAsync()
        {

            if (!_inMemoryDatabase)
            {
                return;
            }

            // Seed Lecturer data
            var profSchmidt = new Lecturer
            {
                Name = "Prof. Dr.",
                Surname = "Schmidt",
                Degree = Degree.Professor
            };

            var drMueller = new Lecturer
            {
                Name = "Dr.",
                Surname = "Müller",
                Degree = Degree.Professor
            };

            await _hochschuleContext.Lecturers.AddRangeAsync(profSchmidt, drMueller);
            await _hochschuleContext.SaveChangesAsync();

            // Seed Semester data
            var winterSemester = new Semester
            {
                Name = "Wintersemester 2024/2025",
                StartDate = new DateTime(2024, 10, 01),
                EndDate = new DateTime(2025, 03, 31)
            };

            var summerSemester = new Semester
            {
                Name = "Sommersemester 2025",
                StartDate = new DateTime(2025, 04, 01),
                EndDate = new DateTime(2025, 09, 30)
            };

            await _hochschuleContext.Semesters.AddRangeAsync(winterSemester, summerSemester);
            await _hochschuleContext.SaveChangesAsync();

            // Seed Course data
            var softwareEngineeringCourse = new Course
            {
                Name = "Software Engineering",
                Description = "Grundlagen der Softwareentwicklung",
                Startdate = new DateTime(2024, 10, 1),
                Enddate = new DateTime(2025, 02, 28),
                Lecturer = profSchmidt
            };

            var databasesCourse = new Course
            {
                Name = "Datenbanken",
                Description = "Datenbankkonzepte und SQL",
                Startdate = new DateTime(2024, 10, 15),
                Enddate = new DateTime(2025, 01, 31),
                Lecturer = drMueller
            };

            await _hochschuleContext.Courses.AddRangeAsync(softwareEngineeringCourse, databasesCourse);
            await _hochschuleContext.SaveChangesAsync();

            // Seed Student data
            var maxMustermann = new Student
            {
                Name = "Max",
                Surname = "Mustermann",
                Birthdate = new DateTime(2000, 1, 15),
                Semester = winterSemester
            };

            var erikaMusterfrau = new Student
            {
                Name = "Erika",
                Surname = "Musterfrau",
                Birthdate = new DateTime(2001, 5, 8),
                Semester = winterSemester
            };

            await _hochschuleContext.Students.AddRangeAsync(maxMustermann, erikaMusterfrau);
            await _hochschuleContext.SaveChangesAsync();

            // Assign students to courses
            softwareEngineeringCourse.Students.Add(maxMustermann);
            softwareEngineeringCourse.Students.Add(erikaMusterfrau);
            databasesCourse.Students.Add(maxMustermann);
            await _hochschuleContext.SaveChangesAsync();

            // Assign courses to semesters
            winterSemester.Courses.Add(softwareEngineeringCourse);
            winterSemester.Courses.Add(databasesCourse);
            await _hochschuleContext.SaveChangesAsync();

            Console.WriteLine("Data seeded successfully.");
        }
    }
}
