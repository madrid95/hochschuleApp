using System;
namespace HochschuleApp.exceptions
{
    /// <summary>
    /// Wird ausgelöst, wenn ein Objekt nicht gefunden wurde.
    /// </summary>
	public class NotFoundException : Exception
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der NotFoundException-Klasse mit der angegebenen Fehlermeldung.
        /// </summary>
        /// <param name="name">entity name.</param>
        /// <param name="id">entity id.</param>
        public NotFoundException(string name, int id) : base($"Entity {name} with ID '{id}' not found.")
        {
        }
    }
}
