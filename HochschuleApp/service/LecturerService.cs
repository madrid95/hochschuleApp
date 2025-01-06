using System;
using HochschuleApp.repository;
using HochschuleApp.entity;
using HochschuleApp.exceptions;
using HochschuleApp.context;

namespace HochschuleApp.service
{
    /// <summary>
    /// Service-Klasse für die Verwaltung von Dozenten-Objekten.
    /// </summary>
    /// <remarks>
    /// Diese Klasse kapselt die Geschäftslogik rund um Dozenten, 
    /// einschließlich der Zuweisung von Kursen.
    /// </remarks>
    public class LecturerService : IHochschuleService<Lecturer, int>
    {
        private readonly CourseRepository _courseRepository;
        private readonly LecturerRepository _lecturerRepository;

        public LecturerService(CourseRepository courseRepository, LecturerRepository lecturerRepository)
        {
            _courseRepository = courseRepository;
            _lecturerRepository = lecturerRepository;
        }

        /// <summary>
        /// Weist einen Dozenten einem Kurs zu.
        /// </summary>
        /// <param name="lecturerId">Die ID des Dozenten.</param>
        /// <param name="courseId">Die ID des Kurses.</param>
        /// <exception cref="InvalidOperationException">Wird geworfen, wenn der Dozent bereits dem Kurs zugewiesen ist.</exception>
        public void AddLecturerToCourse(int lecturerId, int courseId)
        {
            // 1. Retrieve the Lecturer and Course entities
            var lecturer = FindByID(lecturerId);

            var course = _courseRepository.FindByID(courseId) ?? throw new NotFoundException(nameof(Course), courseId);

            // 2. Check if the lecturer is already enrolled in the course
            if (lecturer.Courses.Contains(course))
            {
                throw new InvalidOperationException($"Lecturer with ID '{lecturerId}' is already assinged in Course with ID '{courseId}'.");
            }

            // 3. Add the course to the lecturer's Courses collection
            lecturer.Courses.Add(course);

            _lecturerRepository.Persist();
        }

        /// <summary>
        /// Findet einen Dozenten anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des gesuchten Dozenten.</param>
        /// <returns>Das gefundene Dozent-Objekt oder null, wenn kein Dozent mit der angegebenen ID gefunden wird.</returns>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Dozent mit der angegebenen ID gefunden wird.</exception>
        public Lecturer FindByID(int id)
        {
            return _lecturerRepository.FindByID(id) ?? throw new NotFoundException(nameof(Lecturer), id);
        }

        /// <summary>
        /// Findet eine Liste von Dozenten anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Dozenten.</param>
        /// <returns>Eine Liste der gefundenen Dozenten.</returns>
        public List<Lecturer> FindByIDs(List<int> ids)
        {
            return _lecturerRepository.FindByIDs(ids);
        }

        /// <summary>
        /// Erstellt einen neuen Dozenten.
        /// </summary>
        /// <param name="entity">Das zu erstellende Dozent-Objekt.</param>
        /// <returns>Das neu erstellte Dozent-Objekt.</returns>
        public Lecturer CreateNew(Lecturer entity)
        {
            return _lecturerRepository.Create(entity);
        }

        /// <summary>
        /// Löscht einen Dozenten.
        /// </summary>
        /// <param name="entity">Das zu löschende Dozent-Objekt.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn entity null ist.</exception>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Dozent mit der angegebenen ID gefunden wird.</exception>
        public void Delete(Lecturer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"Entity is null.");
            }

            var foundedIEntity = FindByID(entity.Id);

            _lecturerRepository.Delete(foundedIEntity);
        }

        /// <summary>
        /// Löscht einen Dozenten anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Dozenten.</param>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Dozent mit der angegebenen ID gefunden wird.</exception>
        public void DeleteByID(int id)
        {
            var foundedIEntity = FindByID(id);

            _lecturerRepository.Delete(foundedIEntity);
        }

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Dozenten.
        /// </summary>
        /// <returns>Eine Liste aller Dozenten.</returns>
        public List<Lecturer> ListAll()
        {
            return _lecturerRepository.ListAll();
        }

        /// <summary>
        /// Aktualisiert die Eigenschaften eines bestehenden Dozenten.
        /// </summary>
        /// <param name="id">Die ID des zu aktualisierenden Dozenten.</param>
        /// <param name="newEntity">Das Dozent-Objekt mit den neuen Eigenschaften.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn newEntity null ist.</exception>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Dozent mit der angegebenen ID gefunden wird.</exception>
        public void Update(int id, Lecturer newEntity)
        {
            if (newEntity == null)
            {
                throw new ArgumentNullException(nameof(newEntity), "The entity cannot be null.");
            }

            var oldEntity = FindByID(id);

            _lecturerRepository.Update(oldEntity, newEntity);
        }

        /// <summary>
        /// Prüft, ob ein Dozent mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID des Dozenten.</param>
        /// <returns>True, wenn ein Dozent mit der angegebenen ID existiert, andernfalls False.</returns>
        public bool ExistsByID(int id)
        {
            return _lecturerRepository.ExistsByID(id);
        }
    }
}
