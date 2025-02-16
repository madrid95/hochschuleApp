using System.ComponentModel.DataAnnotations;

namespace HochschuleApp.entity
{
    /// <summary>
    /// Stellt einen Studierenden der Hochschule dar.
    /// </summary>
    public class Student : ICloneable, IIdentifiable<int>, IPrintable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public DateTime? Birthdate { get; set; }
        public Semester? Semester { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        /// <summary> 
        /// Gibt eine Zeichenketten-Darstellung des Student-Objekts zurück.
        /// </summary>
        /// <returns>Eine Zeichenketten-Darstellung des Student-Objekts.</returns>
        public override string ToString()
        {
            return $"{nameof(Student)}: " +      
                   $"Id={Id}, " +
                   $"Surname={Surname}, " +
                   $"Name={Name}, " +
                   $"Address={Address}, " +
                   $"Birthdate={Birthdate?.ToString("yyyy-MM-dd")}, " +
                   $"{(Semester != null ? Semester.ToShortString() : "N/A")}";
        }

        /// <summary>
        /// Erstellt eine flache Kopie des Student-Objekts.
        /// </summary>
        /// <returns>Eine flache Kopie des Student-Objekts.</returns>
        public object Clone()
        {
            // Create a shallow copy of the Semester object
            Student clone = (Student)MemberwiseClone();

            // Clone Courses (shallow copy)
            clone.Courses = new HashSet<Course>(this.Courses);

            return clone;
        }

        /// <summary>
        /// Erstellt einen Klon des Student-Objekts.
        /// </summary>
        /// <returns>Ein Klon des Student-Objekts.</returns>
        public Student CloneObject()
        {
            return (Student)this.Clone();
        }

        /// <summary>
        /// Liefert eine kurze, lesbare Zeichenketten-Darstellung des Studenten.
        /// </summary>
        /// <returns>
        /// Eine Zeichenkette im Format 
        /// "Student: Id=[Id], Surname=[Surname], Name=[Name]".
        /// </returns>
        public string ToShortString()
        {
            return $"{nameof(Student)}: " +
                   $"Id={Id}, " +
                   $"Surname={Surname}, " +
                   $"Name={Name}";
        }
    }
}

