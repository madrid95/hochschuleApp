using System;
using System.Linq;
using HochschuleApp.entity;

namespace HochschuleApp.Extensions
{
    /// <summary>
    /// Erweiterungsmethoden für die Course-Klasse.
    /// </summary>
    public static class CourseExtensions
    {
        /// <summary>
        /// Aktualisiert die Eigenschaften eines Kurs-Objekts mit den Werten eines neuen Kurs-Objekts.
        /// </summary>
        /// <param name="course">Das bestehende Kurs-Objekt, dessen Eigenschaften aktualisiert werden sollen.</param>
        /// <param name="newEntity">Das neue Kurs-Objekt, das die Aktualisierungswerte enthält.</param>
        /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn newEntity null ist.</exception>
        public static void UpdateProperties(this Course course, Course newEntity)
        {
            if (newEntity == null)
            {
                throw new ArgumentNullException(nameof(newEntity));
            }

            course.Name = newEntity.Name;
            course.Description = newEntity.Description;
            course.Startdate = newEntity.Startdate;
            course.Enddate = newEntity.Enddate;
            course.Students.Update(newEntity.Students);
            course.Semesters.Update(newEntity.Semesters);
        }
    }
}

