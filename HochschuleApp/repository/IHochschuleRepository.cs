using System;
using HochschuleApp.entity;

namespace HochschuleApp.repository
{
    /// <summary>
    /// Schnittstelle für ein generisches Repository.
    /// </summary>
    /// <typeparam name="T">Der Typ der Entität.</typeparam>
    /// <typeparam name="ID">Der Typ der eindeutigen Identifikationsnummer der Entität.</typeparam>
    public interface IHochschuleRepository<T, ID> where T : class, IIdentifiable<ID>
    {
        /// <summary>
        /// Findet eine Entität anhand ihrer eindeutigen Identifikationsnummer.
        /// </summary>
        /// <param name="id">Die eindeutige Identifikationsnummer der gesuchten Entität.</param>
        /// <returns>Die gefundene Entität oder null, wenn keine Entität mit der angegebenen ID gefunden wurde.</returns>
        T FindByID(ID id);

        /// <summary>
        /// Findet eine Liste von Entitäten anhand einer Liste von IDs.
        /// </summary>
        /// <param name="ids">Eine Liste von IDs der gesuchten Entitäten.</param>
        /// <returns>Eine Liste der gefundenen Entitäten.</returns>
        List<T> FindByIDs(List<ID> ids);

        /// <summary>
        /// Erstellt eine neue Entität und speichert sie in der Datenbank.
        /// </summary>
        /// <param name="entity">Die zu erstellende Entität.</param>
        /// <returns>Die in der Datenbank gespeicherte Entität.</returns>
        T Create(T entity);

        /// <summary>
        /// Löscht eine Entität aus der Datenbank.
        /// </summary>
        /// <param name="entity">Die zu löschende Entität.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn entity null ist.</exception>
        void Delete(T entity);

        /// <summary>
        /// Löscht eine Entität aus der Datenbank anhand ihrer eindeutigen Identifikationsnummer.
        /// </summary>
        /// <param name="id">Die eindeutige Identifikationsnummer der zu löschenden Entität.</param>
        void DeleteByID(ID id);

        /// <summary>
        /// Liefert eine Liste aller vorhandenen Entitäten.
        /// </summary>
        /// <returns>Eine Liste aller Entitäten.</returns>
        List<T> ListAll();

        /// <summary>
        /// Aktualisiert die Eigenschaften einer bestehenden Entität.
        /// </summary>
        /// <param name="id">Die eindeutige Identifikationsnummer der zu aktualisierenden Entität.</param>
        /// <param name="newEntity">Die Entität mit den neuen Eigenschaften.</param>
        void Update(ID id, T newEntity);

        /// <summary>
        /// Speichert alle Änderungen an den Entitäten im DbContext.
        /// </summary>
        void Persist();

        /// <summary>
        /// Prüft, ob eine Entität mit der angegebenen ID existiert.
        /// </summary>
        /// <param name="id">Die ID der Entität.</param>
        /// <returns>True, wenn eine Entität mit der angegebenen ID existiert, andernfalls False.</returns>
        bool ExistsByID(ID id);
    }
}