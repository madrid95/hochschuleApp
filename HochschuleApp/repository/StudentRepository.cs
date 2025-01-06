using HochschuleApp.context;
using HochschuleApp.entity;
using HochschuleApp.Extensions;

namespace HochschuleApp.repository
{
    /// <summary>
    /// Repository-Klasse für die Verwaltung von Student-Objekten.
    /// </summary>
    /// <remarks>
    /// Diese Klasse kapselt den Datenzugriff für Student-Entitäten und bietet CRUD-Operationen (Create, Read, Update, Delete) sowie weitere spezifische Methoden für die Suche und Verwaltung von Studenten.
    /// Implementiert die `IHochschuleRepository` Schnittstelle für die Entität `Student` mit dem Primärschlüsseltyp `int`.
    /// </remarks>
    public class StudentRepository : IHochschuleRepository<Student, int>
    {
        /// <summary>
        /// Referenz auf den DbContext, der für den Zugriff auf die Datenbank verwendet wird.
        /// </summary>
        private readonly HochschuleContext _hochschuleContext;

        /// <summary>
        /// Konstruktor für die StudentRepository-Klasse.
        /// </summary>
        /// <param name="hochschuleContext">Der zu verwendende HochschuleContext.</param>
        public StudentRepository(HochschuleContext hochschuleContext) =>
            (_hochschuleContext) = (hochschuleContext);

        /// <summary>
        /// Aktualisiert die Eigenschaften eines bestehenden Studenten.
        /// </summary>
        /// <param name="id">Die eindeutige Identifikationsnummer des zu aktualisierenden Studenten.</param>
        /// <param name="oldEntity">Das Student-Objekt mit den alten Eigenschaften.</param>
        /// <param name="newEntity">Das Student-Objekt mit den neuen Eigenschaften.</param>
        public void Update(Student oldEntity, Student newEntity)
        {
            oldEntity.UpdateProperties(newEntity);
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Erstellt einen neuen Studenten und speichert ihn in der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu erstellende Student-Objekt.</param>
        /// <returns>Das in der Datenbank gespeicherte Student-Objekt, einschließlich der zugewiesenen ID.</returns>
        public Student Create(Student entity)
        {
            var storedEntity = _hochschuleContext.Students.Add(entity);
            _hochschuleContext.SaveChanges();
            return storedEntity.Entity;
        }

        /// <summary>
        /// Findet eine Liste von Studenten anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Studenten.</param>
        /// <returns>Eine Liste der gefundenen Studenten-Objekte.</returns>
        public List<Student> FindByIDs(List<int> ids)
        {
            return _hochschuleContext.Students.Where(e => ids.Contains(e.Id)).ToList();
        }

        /// <summary>
        /// Findet einen Studenten anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des gesuchten Studenten.</param>
        /// <returns>Das gefundene Studenten-Objekt oder null, wenn kein Student mit der angegebenen ID gefunden wird.</returns>
        public Student? FindByID(int id)
        {
            return _hochschuleContext.Students.Find(id);
        }

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Studenten.
        /// </summary>
        /// <returns>Eine Liste aller Studenten-Objekte.</returns>
        public List<Student> ListAll()
        {
            return _hochschuleContext.Students.ToList();
        }

        /// <summary>
        /// Löscht ein Student-Objekt aus der Datenbank.
        /// </summary>
        /// <param name="entity">Das zu löschende Student-Objekt.</param>
        public void Delete(Student entity)
        {
            _hochschuleContext.Students.Remove(entity);
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
        /// Prüft, ob ein Student mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID des Studenten.</param>
        /// <returns>True, wenn ein Student mit der angegebenen ID existiert, andernfalls False.</returns>
        public bool ExistsByID(int id)
        {
            return _hochschuleContext.Students.Count(e => e.Id.Equals(id)) == 1;
        }
    }
}

