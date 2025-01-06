using HochschuleApp.context;
using HochschuleApp.entity;
using HochschuleApp.exceptions;
using HochschuleApp.repository;

namespace HochschuleApp.service
{
    /// <summary>
    /// Service-Klasse für die Verwaltung von Kurs-Objekten.
    /// </summary>
    /// <remarks>
    /// Diese Klasse kapselt die Geschäftslogik rund um Kurse, 
    /// einschließlich der Verwaltung von Studentenzuordnungen und Dozentenuweisungen.
    /// </remarks>
    public class CourseService : IHochschuleService<Course, int>
    {
        private readonly CourseRepository _courseRepository;
        private readonly StudentRepository _studentRepository;
        private readonly LecturerRepository _lecturerRepository;

        public CourseService(CourseRepository courseRepository, StudentRepository studentRepository, LecturerRepository lecturerRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _lecturerRepository = lecturerRepository;
        }

        /// <summary>
        /// Fügt einen Studenten zu einem Kurs hinzu.
        /// </summary>
        /// <param name="courseId">Die ID des Kurses.</param>
        /// <param name="studentId">Die ID des Studenten.</param>
        /// <exception cref="InvalidOperationException">Wird geworfen, wenn der Student bereits in dem Kurs eingeschrieben ist.</exception>
        public void AddStudentToCourse(int courseId, int studentId)
        {
            // 1. Retrieve the Course and Student entities
            var course = FindByID(courseId);

            var student = _studentRepository.FindByID(studentId) ?? throw new NotFoundException(nameof(Student), studentId);

            // 2. Check if the student is already enrolled in the course
            if (course.Students.Contains(student))
            {
                throw new InvalidOperationException($"Student with ID '{studentId}' is already enrolled in Course with ID '{courseId}'.");
            }

            // 3. Add the course to the student's Courses collection
            course.Students.Add(student);

            _courseRepository.Persist();
        }

        /// <summary>
        /// Weist einen Dozenten einem Kurs zu.
        /// </summary>
        /// <param name="courseId">Die ID des Kurses.</param>
        /// <param name="lecturerId">Die ID des Dozenten.</param>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Kurs Oder Lecturer mit der angegebenen ID gefunden wird.</exception>
        public void AddLecturerToCourse(int courseId, int lecturerId)
        {
            // 1. Retrieve the Lecturer and Course entities
            var course = FindByID(courseId);

            var lecturer = _lecturerRepository.FindByID(lecturerId) ?? throw new NotFoundException(nameof(Lecturer), lecturerId);

            course.Lecturer = lecturer;

            _lecturerRepository.Persist();
        }

        /// <summary>
        /// Findet einen Kurs anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des gesuchten Kurses.</param>
        /// <returns>Das gefundene Kurs-Objekt oder null, wenn kein Kurs mit der angegebenen ID gefunden wird.</returns>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Kurs mit der angegebenen ID gefunden wird.</exception>
        public Course FindByID(int id)
        {
            return _courseRepository.FindByID(id) ?? throw new NotFoundException(nameof(Course), id);
        }

        /// <summary>
        /// Findet eine Liste von Kursen anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Kurse.</param>
        /// <returns>Eine Liste der gefundenen Kurse.</returns>
        public List<Course> FindByIDs(List<int> ids)
        {
            return _courseRepository.FindByIDs(ids);
        }

        /// <summary>
        /// Erstellt einen neuen Kurs.
        /// </summary>
        /// <param name="entity">Das zu erstellende Kurs-Objekt.</param>
        /// <returns>Das neu erstellte Kurs-Objekt.</returns>
        public Course CreateNew(Course entity)
        {
            return _courseRepository.Create(entity);
        }

        /// <summary>
        /// Löscht einen Kurs aus dem zugrundeliegenden Datenspeicher.
        /// </summary>
        /// <param name="entity">Die zu löschende Kurs-Entität. Darf nicht null sein.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn entity null ist.</exception>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Kurs mit der angegebenen ID gefunden wird.</exception>
        public void Delete(Course entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The entity cannot be null.");
            }

            var foundedIEntity = FindByID(entity.Id);

            _courseRepository.Delete(foundedIEntity);
        }

        /// <summary>
        /// Löscht einen Kurs anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Kurses.</param>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Kurs mit der angegebenen ID gefunden wird.</exception>
        public void DeleteByID(int id)
        {
            var entity = FindByID(id);

            _courseRepository.Delete(entity);
        }

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Kurse.
        /// </summary>
        /// <returns>Eine Liste aller Kurse.</returns>
        public List<Course> ListAll()
        {
            return _courseRepository.ListAll();
        }

        /// <summary>
        /// Aktualisiert die Eigenschaften eines bestehenden Kurses.
        /// </summary>
        /// <param name="id">Die ID des zu aktualisierenden Kurses.</param>
        /// <param name="newEntity">Das Kurs-Objekt mit den neuen Eigenschaften.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn newEntity null ist.</exception>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Kurs mit der angegebenen ID gefunden wird.</exception>
        public void Update(int id, Course newEntity)
        {
            if (newEntity == null)
            {
                throw new ArgumentNullException(nameof(newEntity), "The entity cannot be null.");
            }

            var oldEntity = this.FindByID(id);

            _courseRepository.Update(oldEntity, newEntity);
        }

        /// <summary>
        /// Prüft, ob ein Kurs mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID des Kurses.</param>
        /// <returns>True, wenn ein Kurs mit der angegebenen ID existiert, andernfalls False.</returns>
        public bool ExistsByID(int id)
        {
            return _courseRepository.ExistsByID(id);
        }
    }
}
