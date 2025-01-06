using System;
using HochschuleApp.context;
using HochschuleApp.entity;
using HochschuleApp.exceptions;
using HochschuleApp.Extensions;

namespace HochschuleApp.repository
{
    /// <summary>
    /// Repository-Klasse für die Verwaltung von Kurs-Objekten.
    /// </summary>
    /// <remarks>
    /// Diese Klasse implementiert die `IHochschuleRepository` Schnittstelle für die Entität `Course` mit dem Primärschlüsseltyp `int`.
    /// Sie bietet CRUD-Operationen (Create, Read, Update, Delete) sowie Methoden zum Finden von Kursen nach ID, Kurscode und Listen von IDs.
    /// </remarks>
    public class CourseRepository : IHochschuleRepository<Course, int>
    {
        /// <summary>
        /// Referenz auf den DbContext, der für den Zugriff auf die Datenbank verwendet wird.
        /// </summary>
        private readonly HochschuleContext _hochschuleContext;

        /// <summary>
        /// Konstruktor für die CourseRepository-Klasse.
        /// </summary>
        /// <param name="hochschuleContext">Der zu verwendende HochschuleContext.</param>
        public CourseRepository(HochschuleContext hochschuleContext) =>
            (_hochschuleContext) = (hochschuleContext);

        /// <summary>
        /// Löscht ein Kurs-Objekt aus der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu löschende Kurs-Objekt.</param>
        public void Delete( Course entity)
        {
            _hochschuleContext.Courses.Remove(entity);
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Findet ein Kurs-Objekt anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des gesuchten Kurses.</param>
        /// <returns>Das gefundene Kurs-Objekt oder null, wenn kein Kurs mit der angegebenen ID gefunden wird.</returns>
        public Course? FindByID( int id)
        {
            return _hochschuleContext.Courses.Find(id);
        }

        /// <summary>
        /// Findet eine Liste von Kurs-Objekten anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Kurse.</param>
        /// <returns>Eine Liste der gefundenen Kurs-Objekte.</returns>
        public List<Course> FindByIDs( List<int> ids)
        {
            return _hochschuleContext.Courses.Where(e => ids.Contains(e.Id)).ToList();
        }

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Kurse.
        /// </summary>
        /// <returns>Eine Liste aller Kurs-Objekte.</returns>
        public List<Course> ListAll()
        {
            return _hochschuleContext.Courses.ToList();
        }

        /// <summary>
        /// Erstellt einen neuen Kurs und speichert ihn in der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu erstellende Kurs-Objekt.</param>
        /// <returns>Das in der Datenbank gespeicherte Kurs-Objekt.</returns>
        public Course Create( Course entity)
        {
            var storedEntity = _hochschuleContext.Courses.Add(entity);

            _hochschuleContext.SaveChanges();

            return storedEntity.Entity;
        }

        /// <summary>
        /// Aktualisiert die Eigenschaften eines bestehenden Kurses.
        /// </summary>
        /// <param name="oldEntity">Das Kurs-Objekt mit den alten Eigenschaften.</param>
        /// <param name="newEntity">Das Kurs-Objekt mit den neuen Eigenschaften.</param>
        public void Update(Course oldEntity, Course newEntity)
        {
            oldEntity.UpdateProperties(newEntity);
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Speichert alle Änderungen an den Entitäten im DbContext.
        /// </summary>
        public void Persist()
        {
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Prüft, ob ein Kurs mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID des Kurses.</param>
        /// <returns>True, wenn ein Kurs mit der angegebenen ID existiert, andernfalls False.</returns>
        public bool ExistsByID(int id)
        {
            return _hochschuleContext.Courses.Count(e => e.Id.Equals(id)) == 1;
        }
    }
}

