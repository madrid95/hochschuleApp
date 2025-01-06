using HochschuleApp.entity;

namespace HochschuleApp.Extensions
{
    /// <summary>
    /// Erweiterungsmethoden für die Lecturer-Klasse.
    /// </summary>
	public static class LecturerExtensions
	{
        /// <summary>
        /// Aktualisiert die Eigenschaften eines Dozent-Objekts mit den Werten eines neuen Dozent-Objekts.
        /// </summary>
        /// <param name="lecturer">Das bestehende Dozent-Objekt, dessen Eigenschaften aktualisiert werden sollen.</param>
        /// <param name="newEntity">Das neue Dozent-Objekt, das die Aktualisierungswerte enthält.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn newEntity null ist.</exception>
        public static void UpdateProperties(this Lecturer lecturer, Lecturer newEntity)
        {
            if (newEntity == null)
            {
                throw new ArgumentNullException(nameof(newEntity));
            }

            lecturer.Name = newEntity.Name;
            lecturer.Surname = newEntity.Surname;
            lecturer.Name = newEntity.Name;
            lecturer.Address = newEntity.Address;
            lecturer.Birthdate = newEntity.Birthdate;
            lecturer.Degree = newEntity.Degree;
            lecturer.Courses.Update(newEntity.Courses);
        }
    }
}
