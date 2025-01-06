using System;
namespace HochschuleApp.exceptions
{
    /// <summary>
    /// Wird ausgelöst, wenn ein Objekt mit den gleichen Eigenschaften bereits existiert.
    /// </summary>
    public class AlreadyExistsException : Exception
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der AlreadyExistsException-Klasse mit der angegebenen Fehlermeldung.
        /// </summary>
        /// <param name="name">Klassenname.</param>
        /// <param name="id">id der Entität.</param>
        public AlreadyExistsException(string name, int id) : base($"Entity {name} with ID '{id}' alredy exists.")
        {
        }
    }
}

