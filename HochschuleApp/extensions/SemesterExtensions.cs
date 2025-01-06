using HochschuleApp.entity;

namespace HochschuleApp.Extensions
{
    /// <summary>
    /// Erweiterungsmethoden für die Semester-Klasse.
    /// </summary>
    public static class SemesterExtensions
    {
        /// <summary>
        /// Aktualisiert die Eigenschaften eines Semester-Objekts mit den Werten eines neuen Semester-Objekts.
        /// </summary>
        /// <param name="semester">Das bestehende Semester-Objekt, dessen Eigenschaften aktualisiert werden sollen.</param>
        /// <param name="newEntity">Das neue Semester-Objekt, das die Aktualisierungswerte enthält.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn newEntity null ist.</exception>
        public static void UpdateProperties(this Semester semester, Semester newEntity)
        {
            if (newEntity == null)
            {
                throw new ArgumentNullException(nameof(newEntity));
            }

            semester.Name = newEntity.Name;
            semester.StartDate = newEntity.StartDate;
            semester.EndDate = newEntity.EndDate;
            semester.Courses.Update(newEntity.Courses);
            semester.Students.Update(newEntity.Students);
        }
    }
}
