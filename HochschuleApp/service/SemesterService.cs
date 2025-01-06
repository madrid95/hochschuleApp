using System;
using HochschuleApp.entity;
using HochschuleApp.exceptions;
using HochschuleApp.repository;

namespace HochschuleApp.service
{
    /// <summary>
    /// Service-Klasse für die Verwaltung von Semester-Objekten.
    /// </summary>
    /// <remarks>
    /// Diese Klasse kapselt die Geschäftslogik rund um Semester, 
    /// einschließlich der Zuweisung von Studenten und Kursen.
    /// </remarks>
    public class SemesterService : IHochschuleService<Semester, int>
    {
        private readonly CourseRepository _courseRepository;
        private readonly StudentRepository _studentRepository;
        private readonly SemesterRepository _semesterRepository;

        public SemesterService(CourseRepository courseRepository, StudentRepository studentRepository, SemesterRepository semesterRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _semesterRepository = semesterRepository;
        }

        /// <summary>
        /// Meldet einen Studenten für ein Semester an.
        /// </summary>
        /// <param name="semesterId">Die ID des Semesters.</param>
        /// <param name="studentId">Die ID des Studenten.</param>
        /// <exception cref="InvalidOperationException">Wird geworfen, wenn der Student bereits im Semester angemeldet ist.</exception>
        /// <exception cref="NotFoundException">Wird geworfen, wenn kein Semester mit der angegebenen ID gefunden wird.</exception>
        public void AddStudentToSemester(int semesterId, int studentId)
        {
            // 1. Retrieve the Student and Semester entities
            var semester = FindByID(semesterId);

            var student = _studentRepository.FindByID(studentId) ?? throw new NotFoundException(nameof(Student), studentId);

            // 2. Check if the student is already enrolled in the course
            if (semester.Students.Contains(student))
            {
                throw new InvalidOperationException($"Student with ID '{studentId}' is already added to Semester with ID '{semesterId}'.");
            }

            // 3. Assign the Semester to the Student
            semester.Students.Add(student);

            // 4. Save changes to the database
            _semesterRepository.Persist();
        }

        /// <summary>
        /// Weist einem Semester einen Kurs zu.
        /// </summary>
        /// <param name="semesterId">Die ID des Semesters.</param>
        /// <param name="courseId">Die ID des Kurses.</param>
        /// <exception cref="InvalidOperationException">Wird geworfen, wenn der Kurs bereits im Semester vorhanden ist.</exception>
        /// <exception cref="NotFoundException">Wird geworfen, wenn kein Semester mit der angegebenen ID gefunden wird.</exception>
        public void AddCourseToSemester(int semesterId, int courseId)
        {
            // 1. Retrieve the Course and Semester entities
            var semester = FindByID(semesterId);

            var course = _courseRepository.FindByID(courseId) ?? throw new NotFoundException(nameof(Course), courseId);

            // 2. Check if the course is already assigned in the course
            if (semester.Courses.Contains(course))
            {
                throw new InvalidOperationException($"Course with ID '{courseId}' is already assinged in Semester with ID '{semesterId}'.");
            }

            // 3. Assign the Course to the Semester
            semester.Courses.Add(course);

            // 4. Save changes to the database
            _semesterRepository.Persist();
        }

        /// <summary>
        /// Findet ein Semester anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des gesuchten Semesters.</param>
        /// <returns>Das gefundene Semester-Objekt oder null, wenn kein Semester mit der angegebenen ID gefunden wird.</returns>
        /// <exception cref="NotFoundException">Wird geworfen, wenn kein Semester mit der angegebenen ID gefunden wird.</exception>
        public Semester FindByID(int id)
        {
            return _semesterRepository.FindByID(id) ?? throw new NotFoundException(nameof(Semester), id);
        }

        /// <summary>
        /// Findet eine Liste von Semestern anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Semester.</param>
        /// <returns>Eine Liste der gefundenen Semester.</returns>
        public List<Semester> FindByIDs(List<int> ids)
        {
            return _semesterRepository.FindByIDs(ids);
        }

        /// <summary>
        /// Erstellt ein neues Semester.
        /// </summary>
        /// <param name="entity">Das zu erstellende Semester-Objekt.</param>
        /// <returns>Das neu erstellte Semester-Objekt.</returns>
        public Semester CreateNew(Semester entity)
        {
            return _semesterRepository.Create(entity);
        }

        /// <summary>
        /// Löscht ein Semester.
        /// </summary>
        /// <param name="entity">Das zu löschende Semester-Objekt.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn entity null ist.</exception>
        /// <exception cref="NotFoundException">Wird geworfen, wenn kein Semester mit der angegebenen ID gefunden wird.</exception>
        public void Delete(Semester entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The entity cannot be null.");
            }

            var foundedIEntity = FindByID(entity.Id);
            _semesterRepository.Delete(foundedIEntity);
        }

        /// <summary>
        /// Löscht ein Semester anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Semesters.</param>
        /// <exception cref="NotFoundException">Wird geworfen, wenn kein Semester mit der angegebenen ID gefunden wird.</exception>
        public void DeleteByID(int id)
        {
            var entity = FindByID(id);

            _semesterRepository.Delete(entity);
        }

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Semester.
        /// </summary>
        /// <returns>Eine Liste aller Semester.</returns>
        public List<Semester> ListAll()
        {
            return _semesterRepository.ListAll();
        }

        /// <summary>
        /// Aktualisiert die Eigenschaften eines bestehenden Semesters.
        /// </summary>
        /// <param name="id">Die ID des zu aktualisierenden Semesters.</param>
        /// <param name="newEntity">Das Semester-Objekt mit den neuen Eigenschaften.</param>
        public void Update(int id, Semester newEntity)
        {
            if (newEntity == null)
            {
                throw new ArgumentNullException(nameof(newEntity), "The entity cannot be null.");
            }

            var oldEntity = this.FindByID(id);

            _semesterRepository.Update(oldEntity, newEntity);
        }

        /// <summary>
        /// Prüft, ob ein Semester mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID des Semesters.</param>
        /// <returns>True, wenn ein Semester mit der angegebenen ID existiert, andernfalls False.</returns>
        public bool ExistsByID(int id)
        {
            return _semesterRepository.ExistsByID(id);
        }
    }
}
