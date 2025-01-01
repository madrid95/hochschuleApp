using System;
using System.Security.Cryptography;

namespace HochschuleApp.entity
{
    /// <summary>
    /// Stellt ein Interface dar, das eine eindeutige Kennung für Entitäten definiert.
    /// </summary>
    /// <typeparam name="ID">Der Datentyp der eindeutigen Kennung.</typeparam>
    public interface IIdentifiable<ID>
	{
        ID Id { get; set; }
    }
}
