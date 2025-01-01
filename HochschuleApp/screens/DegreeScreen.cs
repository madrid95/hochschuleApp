using System;
using HochschuleApp.entity;

namespace HochschuleApp.screens
{
    /// <summary>
    /// Hilfsmethoden für die Auswahl von Studiengängen.
    /// </summary>
    public static class DegreeScreen
    {
        /// <summary>
        /// Fordert den Benutzer zur Auswahl eines Studiengangs auf und gibt den ausgewählten Studiengang zurück.
        /// </summary>
        /// <returns>Der vom Benutzer ausgewählte Studiengang.</returns>
        public static Degree GetDegreeFromUser()
        {
            Console.WriteLine("Select Degree:");
            Console.WriteLine("1. Bachelor");
            Console.WriteLine("2. Master");
            Console.WriteLine("3. PhD");
            Console.WriteLine("4. Professor");

            int choice = InputScreen.GetIntInputBetween("Enter your choice (1-4):", 1, 4);

            return (Degree)choice - 1; // Adjust index to match enum values
        }

        /// <summary>
        /// Fordert den Benutzer zur Auswahl eines Studiengangs auf und gibt den ausgewählten Studiengang oder einen Standardwert zurück.
        /// </summary>
        /// <param name="degree">Der Standardwert für den Studiengang.</param>
        /// <returns>Der vom Benutzer ausgewählte Studiengang oder der Standardwert.</returns>
        public static Degree GetDegreeFromUserWithDefaultValue(Degree degree)
        {
            Console.WriteLine("Select Degree:");
            Console.WriteLine("1. Bachelor");
            Console.WriteLine("2. Master");
            Console.WriteLine("3. PhD");
            Console.WriteLine("4. Professor");

            int choice = InputScreen.GetIntInputBetweenWithDefaultValue("Enter your choice (1-4):", 1, 4, (int)degree + 1);

            return (Degree)choice - 1; // Adjust index to match enum values
        }
    }
}