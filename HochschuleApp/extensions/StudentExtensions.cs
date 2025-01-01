using System;
using HochschuleApp.entity;

namespace HochschuleApp.Extensions
{
    /// <summary>
    /// Erweiterungsmethoden für die Student-Klasse.
    /// </summary>
    public static class StudentExtensions
    {
        /// <summary>
        /// Aktualisiert die Eigenschaften eines Studenten-Objekts mit den Werten eines neuen Studenten-Objekts.
        /// </summary>
        /// <param name="student">Das bestehende Studenten-Objekt, dessen Eigenschaften aktualisiert werden sollen.</param>
        /// <param name="newEntity">Das neue Studenten-Objekt, das die Aktualisierungswerte enthält.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn newEntity null ist.</exception>
        public static void UpdateProperties(this Student student, Student newEntity)
        {
            if (newEntity == null)
            {
                throw new ArgumentNullException(nameof(newEntity));
            }

            student.Surname = newEntity.Surname;
            student.Name = newEntity.Name;
            student.Address = newEntity.Address;
            student.Birthdate = newEntity.Birthdate;
            student.Semester = newEntity.Semester;
            student.Courses.Update(newEntity.Courses);
        }
    }
}

