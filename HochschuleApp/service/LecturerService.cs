using System;
using HochschuleApp.repository;
using HochschuleApp.entity;
using HochschuleApp.exceptions;

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
            var lecturer = _lecturerRepository.FindByID(lecturerId);

            var course = _courseRepository.FindByID(courseId);

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
        public Lecturer FindByID(int id)
        {
            return _lecturerRepository.FindByID(id);
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
        public void Delete(Lecturer entity)
        {
            _lecturerRepository.Delete(entity);
        }

        /// <summary>
        /// Löscht einen Dozenten anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Dozenten.</param>
        public void DeleteByID(int id)
        {
            _lecturerRepository.DeleteByID(id);
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
        public void Update(int id, Lecturer newEntity)
        {
            _lecturerRepository.Update(id, newEntity);
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
