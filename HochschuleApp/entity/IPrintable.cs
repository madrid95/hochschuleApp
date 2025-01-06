using System;
namespace HochschuleApp.entity
{
    /// <summary>
    /// Stellt ein Interface dar, das eine Methode definiert.
    /// </summary>
    public interface IPrintable
	{
        /// <summary>
        /// Liefert eine kurze, lesbare Zeichenketten-Darstellung des Objekts.
        /// </summary>
        /// <returns>Eine kurze Zeichenkette, die das Objekt repräsentiert.</returns>
        string ToShortString();
	}
}
