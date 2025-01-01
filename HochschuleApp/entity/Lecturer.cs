using System.ComponentModel.DataAnnotations;

namespace HochschuleApp.entity
{
    /// <summary>
    /// Stellt einen Dozenten/eine Dozentin der Hochschule dar.
    /// </summary>
	public class Lecturer : ICloneable, IIdentifiable<int>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime? Birthdate { get; set; }
        public Degree Degree { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        /// <summary>
        /// Gibt eine Zeichenketten-Darstellung des Lecturer-Objekts zurück.
        /// </summary>
        /// <returns>Eine Zeichenketten-Darstellung des Lecturer-Objekts.</returns>
        public override string ToString()
        {
            return $"{nameof(Lecturer)}: " +
                   $"Id={Id}, " +
                   $"Surname={Surname}, " +
                   $"Name={Name}, " +
                   $"Address={Address}, " +
                   $"Birthdate={Birthdate?.ToString("yyyy-MM-dd")}, " +
                   $"Degree={Degree}";
        }

        /// <summary>
        /// Erstellt eine flache Kopie des Lecturer-Objekts.
        /// </summary>
        /// <returns>Eine flache Kopie des Lecturer-Objekts.</returns>
        public object Clone()
        {
            // Create a shallow copy of the Semester object
            Lecturer clone = (Lecturer)MemberwiseClone();

            // Clone Courses (shallow copy)
            clone.Courses = new HashSet<Course>(this.Courses);

            return clone;
        }

        /// <summary>
        /// Erstellt einen Klon des Lecturer-Objekts.
        /// </summary>
        /// <returns>Ein Klon des Lecturer-Objekts.</returns>
        public Lecturer CloneObject()
        {
            return (Lecturer)this.Clone();
        }
    }
}

