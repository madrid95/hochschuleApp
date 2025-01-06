namespace HochschuleApp.Extensions
{
    /// <summary>
    /// Erweiterungsmethoden für ICollection.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Aktualisiert eine vorhandene Sammlung mit den Elementen einer neuen Sammlung.
        /// </summary>
        /// <typeparam name="T">Typ der Elemente in der Sammlung.</typeparam>
        /// <param name="existingCollection">Die vorhandene Sammlung.</param>
        /// <param name="newCollection">Die neue Sammlung.</param>
        /// <returns>Die aktualisierte Sammlung.</returns>
        public static ICollection<T> Update<T>(this ICollection<T> existingCollection, ICollection<T> newCollection) where T : class
        {
            // Remove items from the existing collection if they are not present in the new collection
            foreach (var itemToRemove in existingCollection.ToList())
            {
                if (!newCollection.Contains(itemToRemove))
                {
                    existingCollection.Remove(itemToRemove);
                }
            }

            // Add items to the existing collection if they are not already present
            foreach (var itemToAdd in newCollection)
            {
                if (!existingCollection.Contains(itemToAdd))
                {
                    existingCollection.Add(itemToAdd);
                }
            }

            return existingCollection;
        }
    }
}
