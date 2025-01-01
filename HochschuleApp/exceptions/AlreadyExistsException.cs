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
        /// <param name="message">Die Fehlermeldung.</param>
        public AlreadyExistsException(string message) : base(message)
        {
        }
    }
}

