using System.ComponentModel.DataAnnotations;
using HochschuleApp.screens;

namespace HochschuleApp.entity
{
    /// <summary>
    /// Stellt ein Semester an der Hochschule dar.
    /// </summary>
    public class Semester : ICloneable, IIdentifiable<int>, IPrintable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        /// <summary>
        /// Gibt eine Zeichenketten-Darstellung des Semester-Objekts zurück.
        /// </summary>
        /// <returns>Eine Zeichenketten-Darstellung des Semester-Objekts.</returns>
        public override string ToString()
        {
            return $"{nameof(Semester)}: " +
                   $"Id={Id}, " +
                   $"Name={Name}, " +
                   $"Startdate={StartDate?.ToString(InputScreen.DateFormat)}, " +
                   $"Enddate={EndDate?.ToString(InputScreen.DateFormat)}";
        }

        /// <summary>
        /// Erstellt eine flache Kopie des Semester-Objekts.
        /// </summary>
        /// <returns>Eine flache Kopie des Semester-Objekts.</returns>
        public object Clone()
        {
            // Create a shallow copy of the Semester object
            Semester clone = (Semester)MemberwiseClone();

            // Clone Courses (shallow copy)
            clone.Courses = new HashSet<Course>(this.Courses);

            // Clone Students (shallow copy)
            clone.Students = new HashSet<Student>(this.Students);

            return clone;
        }

        /// <summary>
        /// Erstellt einen Klon des Semester-Objekts.
        /// </summary>
        /// <returns>Ein Klon des Semester-Objekts.</returns>
        public Semester CloneObject()
        {
            return (Semester)this.Clone();
        }

        /// <summary>
        /// Liefert eine kurze, lesbare Zeichenketten-Darstellung des Semesters.
        /// </summary>
        /// <returns>
        /// Eine Zeichenkette im Format 
        /// "Semester: Id=[Id], Name=[Name]".
        /// </returns>
        public string ToShortString()
        {
            return $"{nameof(Semester)}: " +
                   $"Id={Id}, " +
                   $"Name={Name}";
        }
    }
}

