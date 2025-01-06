using System;
using HochschuleApp.context;
using HochschuleApp.entity;
using HochschuleApp.Extensions;

namespace HochschuleApp.repository
{
    /// <summary>
    /// Repository-Klasse für die Verwaltung von Dozent-Objekten.
    /// </summary>
    /// <remarks>
    /// Diese Klasse kapselt den Datenzugriff für Dozent-Entitäten und bietet CRUD-Operationen (Create, Read, Update, Delete) sowie weitere spezifische Methoden für die Suche und Verwaltung von Dozenten.
    /// Implementiert die `IHochschuleRepository` Schnittstelle für die Entität `Lecturer` mit dem Primärschlüsseltyp `int`.
    /// </remarks>
    public class LecturerRepository : IHochschuleRepository<Lecturer, int>
    {
        // <summary>
        /// Referenz auf den DbContext, der für den Zugriff auf die Datenbank verwendet wird.
        /// </summary>
        private readonly HochschuleContext _hochschuleContext;

        /// <summary>
        /// Konstruktor für die LecturerRepository-Klasse.
        /// </summary>
        /// <param name="hochschuleContext">Der zu verwendende HochschuleContext.</param>
        public LecturerRepository(HochschuleContext hochschuleContext) =>
            (_hochschuleContext) = (hochschuleContext);

        /// <summary>
        /// Löscht ein Dozent-Objekt aus der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu löschende Dozent-Objekt.</param>
        public void Delete(Lecturer entity)
        {
            _hochschuleContext.Lecturers.Remove(entity);
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Findet ein Dozent-Objekt anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des gesuchten Dozenten.</param>
        /// <returns>Das gefundene Dozent-Objekt oder null, wenn kein Dozent mit der angegebenen ID gefunden wird.</returns>
        public Lecturer? FindByID(int id)
        {
            return _hochschuleContext.Lecturers.Find(id);
        }

        /// <summary>
        /// Findet eine Liste von Dozent-Objekten anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Dozenten.</param>
        /// <returns>Eine Liste der gefundenen Dozent-Objekte.</returns>
        public List<Lecturer> FindByIDs(List<int> ids)
        {
            return _hochschuleContext.Lecturers.Where(e => ids.Contains(e.Id)).ToList();
        }

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Dozenten.
        /// </summary>
        /// <returns>Eine Liste aller Dozent-Objekte.</returns>
        public List<Lecturer> ListAll()
        {
            return _hochschuleContext.Lecturers.ToList();
        }

        /// <summary>
        /// Erstellt einen neuen Dozenten und speichert ihn in der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu erstellende Dozent-Objekt.</param>
        /// <returns>Das in der Datenbank gespeicherte Dozent-Objekt, einschließlich der zugewiesenen ID.</returns>
        public Lecturer Create(Lecturer entity)
        {
            var storedEntity = _hochschuleContext.Lecturers.Add(entity);
            _hochschuleContext.SaveChanges();
            return storedEntity.Entity;
        }

        /// <summary>
        /// Aktualisiert die Eigenschaften eines bestehenden Dozenten.
        /// </summary>
        /// <param name="oldEntity">Das Dozent-Objekt mit den alten Eigenschaften.</param>
        /// <param name="newEntity">Das Dozent-Objekt mit den neuen Eigenschaften.</param>
        public void Update(Lecturer oldEntity, Lecturer newEntity)
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
        /// Prüft, ob ein Dozent mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID des Dozenten.</param>
        /// <returns>True, wenn ein Dozent mit der angegebenen ID existiert, andernfalls False.</returns>
        public bool ExistsByID(int id)
        {
            return _hochschuleContext.Lecturers.Count(e => e.Id.Equals(id)) == 1;
        }
    }
}
