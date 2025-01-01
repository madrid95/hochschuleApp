using System;
using System.Data.Entity;
using HochschuleApp.context;
using HochschuleApp.entity;
using HochschuleApp.exceptions;
using HochschuleApp.Extensions;
using Microsoft.EntityFrameworkCore;

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
        /// <param name="newEntity">Das Student-Objekt mit den neuen Eigenschaften.</param>
        /// <exception cref="NotFoundException">Wird geworfen, wenn kein Student mit der angegebenen ID gefunden wird.</exception>
        public void Update(int id, Student newEntity)
        {
            var foundEntity = this.FindByID(id);
            foundEntity.UpdateProperties(newEntity);
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
        /// <exception cref="NotFoundException">Wird geworfen, wenn kein Student mit der angegebenen ID gefunden wird.</exception>
        public Student FindByID(int id)
        {
            return _hochschuleContext.Students.Find(id) ?? throw new NotFoundException($"Entity with ID '{id}' not found.");
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
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn `entity` null ist.</exception>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Student mit der angegebenen ID gefunden wird.</exception>
        /// <remarks>
        /// **Hinweis:** Das Löschen eines Studenten kann Auswirkungen auf andere Entitäten haben, wie z.B. Einschreibungen in Kurse.
        /// Bitte stellen Sie sicher, dass alle Abhängigkeiten vor dem Löschen gelöst wurden.
        /// </remarks>
        public void Delete(Student entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"Entity is null.");
            }

            var foundedIEntity = _hochschuleContext.Students.Find(entity.Id)
                ?? throw new NotFoundException($"Entity with ID '{entity.Id}' not found.");

            _hochschuleContext.Students.Remove(foundedIEntity);
            _hochschuleContext.SaveChanges();
        }

        /// <summary>
        /// Löscht einen Studenten anhand seiner ID.
        /// </summary>
        /// <param name="id">Die ID des zu löschenden Studenten.</param>
        /// <exception cref="NotFoundException">Wird ausgelöst, wenn kein Student mit der angegebenen ID gefunden wird.</exception>
        public void DeleteByID(int id)
        {
            var entity = this.FindByID(id);
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

