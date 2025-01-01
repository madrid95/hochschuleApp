using System;
using HochschuleApp.entity;
using HochschuleApp.exceptions;
using HochschuleApp.repository;

namespace HochschuleApp.service
{
    /// <summary>
    /// Service-Klasse für die Verwaltung von Studenten-Objekten.
    /// </summary>
    /// <remarks>
    /// Diese Klasse kapselt die Geschäftslogik rund um Studenten, 
    /// einschließlich der Zuweisung von Kursen und Semestern.
    /// </remarks>
    public class StudentService : IHochschuleService<Student, int>
    {
        private readonly CourseRepository _courseRepository;
        private readonly StudentRepository _studentRepository;
        private readonly SemesterRepository _semesterRepository;

        public StudentService(CourseRepository courseRepository, StudentRepository studentRepository, SemesterRepository semesterRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _semesterRepository = semesterRepository;
        }

        /// <summary>
        /// Meldet einen Studenten für einen Kurs an.
        /// </summary>
        /// <param name="studentId">Die ID des Studenten.</param>
        /// <param name="courseId">Die ID des Kurses.</param>
        /// <exception cref="InvalidOperationException">Wird geworfen, wenn der Student bereits im Kurs angemeldet ist.</exception>
        public void AddStudentToCourse(int studentId, int courseId)
        {
            // 1. Retrieve the Student and Course entities
            var student = _studentRepository.FindByID(studentId);

            var course = _courseRepository.FindByID(courseId);

            // 2. Check if the student is already enrolled in the course
            if (student.Courses.Contains(course))
            {
                throw new InvalidOperationException($"Student with ID '{studentId}' is already enrolled in Course with ID '{courseId}'.");
            }

            // 3. Add the course to the student's Courses collection
            student.Courses.Add(course);

            _studentRepository.Persist();
        }

        /// <summary>
        /// Meldet einen Studenten für ein Semester an.
        /// </summary>
        /// <param name="studentId">Die ID des Studenten.</param>
        /// <param name="semesterId">Die ID des Semesters.</param>
        public void AddStudentToSemester(int studentId, int semesterId)
        {
            // 1. Retrieve the Student and Semester entities
            var student = _studentRepository.FindByID(studentId);
            var semester = _semesterRepository.FindByID(semesterId);

            // 2. Assign the Semester to the Student
            student.Semester = semester;

            // 3. Save changes to the database
            _studentRepository.Persist();
        }

        /// <summary>
        /// Findet einen Studenten anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des gesuchten Studenten.</param>
        /// <returns>Das gefundene Studenten-Objekt oder null, wenn kein Student mit der angegebenen ID gefunden wird.</returns>
        public Student FindByID(int id)
        {
            return _studentRepository.FindByID(id);
        }

        /// <summary>
        /// Findet eine Liste von Studenten anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Studenten.</param>
        /// <returns>Eine Liste der gefundenen Studenten.</returns>
        public List<Student> FindByIDs(List<int> ids)
        {
            return _studentRepository.FindByIDs(ids);
        }

        /// <summary>
        /// Erstellt einen neuen Studenten.
        /// </summary>
        /// <param name="entity">Das zu erstellende Studenten-Objekt.</param>
        /// <returns>Das neu erstellte Studenten-Objekt.</returns>
        public Student CreateNew(Student entity)
        {
            return _studentRepository.Create(entity);
        }

        /// <summary>
        /// Löscht einen Studenten.
        /// </summary>
        /// <param name="entity">Das zu löschende Studenten-Objekt.</param>
        public void Delete(Student entity)
        {
            _studentRepository.Delete(entity);
        }

        /// <summary>
        /// Löscht einen Studenten anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Studenten.</param>
        public void DeleteByID(int id)
        {
            _studentRepository.DeleteByID(id);
        }

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Studenten.
        /// </summary>
        /// <returns>Eine Liste aller Studenten.</returns>
        public List<Student> ListAll()
        {
            return _studentRepository.ListAll();
        }

        /// <summary>
        /// Aktualisiert die Eigenschaften eines bestehenden Studenten.
        /// </summary>
        /// <param name="id">Die ID des zu aktualisierenden Studenten.</param>
        /// <param name="newEntity">Das Student-Objekt mit den neuen Eigenschaften.</param>
        public void Update(int id, Student newEntity)
        {
            _studentRepository.Update(id, newEntity);
        }

        /// <summary>
        /// Prüft, ob ein Student mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID des Studenten.</param>
        /// <returns>True, wenn ein Student mit der angegebenen ID existiert, andernfalls False.</returns>
        public bool ExistsByID(int id)
        {
            return _studentRepository.ExistsByID(id);
        }
    }
}
