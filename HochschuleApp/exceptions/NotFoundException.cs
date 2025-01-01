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
        /// <param name="message">Die Fehlermeldung.</param>
        public NotFoundException(string message) : base(message)
        {
        }
    }
}

