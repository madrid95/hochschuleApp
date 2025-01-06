using System.ComponentModel.DataAnnotations;
using HochschuleApp.screens;

namespace HochschuleApp.entity
{
    /// <summary>
    /// Stellt ein Kurs-Objekt innerhalb der HochschuleApp dar.
    /// </summary>
    public class Course : ICloneable, IIdentifiable<int>, IPrintable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public Lecturer? Lecturer { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }

        /// <summary>
        /// Sammlung von Studierenden, die an dem Kurs teilnehmen.
        /// </summary>
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        /// <summary>
        /// Sammlung von Semestern, in denen der Kurs angeboten wird.
        /// </summary>
        public virtual ICollection<Semester> Semesters { get; set; } = new HashSet<Semester>();


        /// <summary>
        /// Gibt eine Zeichenketten-Darstellung des Course-Objekts zurück.
        /// </summary>
        /// <returns>Eine Zeichenketten-Darstellung des Course-Objekts.</returns>
        public override string ToString()
        {
            return $"{nameof(Course)}: " +
                   $"Id={Id}, " +
                   $"Name={Name}, " +
                   $"Description={Description}, " +
                   $"Lecturer={(Lecturer != null ? Lecturer.ToShortString() : "N/A")}, " +
                   $"Startdate={Startdate?.ToString(InputScreen.DateFormat)}, " +
                   $"Enddate={Enddate?.ToString(InputScreen.DateFormat)}";
        }

        /// <summary>
        /// Liefert eine kurze, lesbare Zeichenketten-Darstellung des Kurses.
        /// </summary>
        /// <returns>
        /// Eine Zeichenkette im Format 
        /// "Course: Id=[Id], Name=[Name]".
        /// </returns>
        public string ToShortString()
        {
            return $"{nameof(Course)}: " +
                   $"Id={Id}, " +
                   $"Name={Name}";
        }

        /// <summary>
        /// Erstellt eine flache Kopie des Course-Objekts.
        /// </summary>
        /// <returns>Eine flache Kopie des Course-Objekts.</returns>
        public object Clone()
        {
            // Create a shallow copy of the Semester object
            Course clone = (Course)MemberwiseClone();

            // Clone Students (shallow copy)
            clone.Students = new HashSet<Student>(this.Students);

            // Clone Semesters (shallow copy)
            clone.Semesters = new HashSet<Semester>(this.Semesters);

            return clone;
        }

        /// <summary>
        /// Erstellt einen Klon des Course-Objekts.
        /// </summary>
        /// <returns>Ein Klon des Course-Objekts.</returns>
        public Course CloneObject()
        {
            return (Course)this.Clone();
        }
    }
}