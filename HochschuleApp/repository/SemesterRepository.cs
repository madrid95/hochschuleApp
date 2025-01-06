using System;
using HochschuleApp.entity;
using HochschuleApp.context;
using HochschuleApp.Extensions;

namespace HochschuleApp.repository
{
    /// <summary>
    /// Repository-Klasse für die Verwaltung von Semester-Objekten.
    /// </summary>
    /// <remarks>
    /// Diese Klasse kapselt den Datenzugriff für Semester-Entitäten und bietet CRUD-Operationen (Create, Read, Update, Delete) sowie weitere spezifische Methoden für die Suche und Verwaltung von Semestern.
    /// Implementiert die `IHochschuleRepository` Schnittstelle für die Entität `Semester` mit dem Primärschlüsseltyp `int`.
    /// </remarks>
    public class SemesterRepository : IHochschuleRepository<Semester, int>
    {
        /// <summary>
        /// Referenz auf den DbContext, der für den Zugriff auf die Datenbank verwendet wird.
        /// </summary>
        private readonly HochschuleContext _hochschuleContext;

        /// <summary>
        /// Konstruktor für die SemesterRepository-Klasse.
        /// </summary>
        /// <param name="hochschuleContext">Der zu verwendende HochschuleContext.</param>
        public SemesterRepository(HochschuleContext hochschuleContext) =>
            (_hochschuleContext) = (hochschuleContext);

        /// <summary>
        /// Aktualisiert die Eigenschaften eines bestehenden Semesters.
        /// </summary>
        /// <param name="id">Die eindeutige Identifikationsnummer des zu aktualisierenden Semesters.</param>
        /// <param name="oldEntity">Das Semester-Objekt mit den alten Eigenschaften.</param>
        /// <param name="newEntity">Das Semester-Objekt mit den neuen Eigenschaften.</param>
        public void Update(Semester oldEntity, Semester newEntity)
        {
            oldEntity.UpdateProperties(newEntity);
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Erstellt ein neues Semester und speichert ihn in der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu erstellende Semester-Objekt.</param>
        /// <returns>Das in der Datenbank gespeicherte Semester-Objekt, einschließlich der zugewiesenen ID.</returns>
        public Semester Create(Semester entity)
        {
            var storedEntity = _hochschuleContext.Semesters.Add(entity);
            _hochschuleContext.SaveChanges();
            return storedEntity.Entity;
        }

        /// <summary>
        /// Findet eine Liste von Semestern anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Semester.</param>
        /// <returns>Eine Liste der gefundenen Semester-Objekte.</returns>
        public List<Semester> FindByIDs(List<int> ids)
        {
            return _hochschuleContext.Semesters.Where(e => ids.Contains(e.Id)).ToList();
        }

        /// <summary>
        /// Findet ein Semester anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des gesuchten Semesters.</param>
        /// <returns>Das gefundene Semester-Objekt oder null, wenn kein Semester mit der angegebenen ID gefunden wird.</returns>
        public Semester? FindByID(int id)
        {
            return _hochschuleContext.Semesters.Find(id);
        }

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Semester.
        /// </summary>
        /// <returns>Eine Liste aller Semester-Objekte.</returns>
        public List<Semester> ListAll()
        {
            return _hochschuleContext.Semesters.ToList();
        }

        /// <summary>
        /// Löscht ein Semester-Objekt aus der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu löschende Semester-Objekt.</param>
        public void Delete(Semester entity)
        {
            _hochschuleContext.Semesters.Remove(entity);
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Speichert alle Änderungen an den Entitäten im DbContext.
        /// </summary>
        /// <remarks>
        /// Diese Methode sollte nach jeder Änderung an den Entitäten aufgerufen werden, um die Änderungen in der Datenbank zu persistieren.
        /// </remarks>
        public void Persist()
        {
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Prüft, ob ein Semester mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID des Semesters.</param>
        /// <returns>True, wenn ein Semester mit der angegebenen ID existiert, andernfalls False.</returns>
        public bool ExistsByID(int id)
        {
            return _hochschuleContext.Semesters.Count(e => e.Id.Equals(id)) == 1;
        }
    }
}
