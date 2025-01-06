using HochschuleApp.entity;

namespace HochschuleApp.screens
{
    /// <summary>
    /// Hilfsklasse für die Eingabeverarbeitung in der Konsole.
    /// </summary>
    public static class InputScreen
	{
        /// <summary>
        /// Das Datumsformat, das für Datumsangaben in der Konsole erwartet wird.
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Fordert den Benutzer auf, eine Zeichenfolge einzugeben, und gibt die Eingabe zurück.
        /// </summary>
        /// <param name="prompt">Die Eingabeaufforderung, die dem Benutzer angezeigt wird.</param>
        /// <returns>Die eingegebene Zeichenfolge. Wenn der Benutzer keine Eingabe macht, wird ein leerer String zurückgegeben.</returns>
        public static string GetStringInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Fordert den Benutzer auf, eine Zeichenfolge einzugeben, und gibt die Eingabe mit einem Standardwert zurück.
        /// </summary>
        /// <param name="input">Die eingegebene Zeichenfolge (kann null sein).</param>
        /// <param name="defaultValue">Der Standardwert, der zurückgegeben wird, wenn keine Eingabe erfolgt (optional, Standardwert ist ein leerer String).</param>
        /// <returns>Die eingegebene Zeichenfolge oder den Standardwert, wenn keine Eingabe erfolgt.</returns>
        public static string GetStringInputWithDefaultValue(string input, string defaultValue = "")
        {
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }

        /// <summary>
        /// Fordert den Benutzer auf, eine ganze Zahl einzugeben, und gibt die Eingabe mit einem Standardwert zurück.
        /// </summary>
        /// <param name="prompt">Die Eingabeaufforderung, die dem Benutzer angezeigt wird (z.B. "Alter eingeben: ").</param>
        /// <param name="defaultValue">Der Standardwert, der zurückgegeben wird, wenn keine gültige Eingabe erfolgt (optional, Standardwert ist 0).</param>
        /// <returns>Die eingegebene ganze Zahl. Wenn keine gültige Eingabe erfolgt oder der Benutzer keine Eingabe macht, wird der Standardwert zurückgegeben.</returns>
        public static int? GetIntInputWithDefaultValue(string prompt, int? defaultValue)
        {
            Console.Write($"{prompt} ({defaultValue}): ");
            int result;
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine();

                if(string.IsNullOrEmpty(input))
                {
                    return defaultValue;
                }

                if (int.TryParse(input, out result))
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
            return result;
        }

        /// <summary>
        /// Fordert den Benutzer auf, eine Zeichenfolge einzugeben, und entfernt dabei führende und nachfolgende Leerzeichen.
        /// </summary>
        /// <param name="prompt">Die Eingabeaufforderung, die dem Benutzer angezeigt wird (z.B. "Vorname eingeben: ").</param>
        /// <returns>Die eingegebene Zeichenfolge ohne führende und nachfolgende Leerzeichen. Wenn der Benutzer keine Eingabe macht, wird ein leerer String zurückgegeben.</returns>
        public static string GetStringTrimInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Fordert den Benutzer auf, eine ganze Zahl einzugeben, und gibt die Eingabe zurück.
        /// </summary>
        /// <param name="prompt">Die Eingabeaufforderung, die dem Benutzer angezeigt wird (z.B. "ID eingeben: ").</param>
        /// <returns>Die eingegebene ganze Zahl. Wenn keine gültige Eingabe erfolgt, wird eine Fehlermeldung ausgegeben und die Eingabe wiederholt.</returns>
        public static int GetIntInput(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
            return result;
        }

        /// <summary>
        /// Ermittelt eine ganze Zahl vom Benutzer.
        /// </summary>
        /// <remarks>
        /// Fordert den Benutzer so lange auf, eine ganze Zahl einzugeben,
        /// bis eine gültige Zahl eingegeben wurde oder der Benutzer eine leere Eingabe tätigt.
        /// </remarks>
        /// <param name="prompt">Die anzuzeigende Eingabeaufforderung.</param>
        /// <returns>
        /// Die eingegebene ganze Zahl, oder null, wenn keine Zahl eingegeben wurde.
        /// </returns>
        public static int? GetIntInputOrNull(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    return null;
                }

                if (int.TryParse(input, out int result))
                {
                    return result;
                }

                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        /// <summary>
        /// Fordert den Benutzer auf, eine ganze Zahl innerhalb eines bestimmten Bereichs einzugeben.
        /// </summary>
        /// <param name="prompt">Die Eingabeaufforderung, die dem Benutzer angezeigt wird.</param>
        /// <param name="min">Die untere Grenze des zulässigen Wertebereichs (optional, Standardwert: int.MinValue).</param>
        /// <param name="max">Die obere Grenze des zulässigen Wertebereichs (optional, Standardwert: int.MaxValue).</param>
        /// <returns>Die eingegebene ganze Zahl, die innerhalb des angegebenen Bereichs liegt.</returns>
        /// <exception cref="ArgumentException">Wird geworfen, wenn der Mindestwert größer als der Höchstwert ist.</exception>
        public static int GetIntInputBetween(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= min && result <= max)
                {
                    return result;
                }
                Console.WriteLine($"Invalid input. Please enter an integer between {min} and {max}.");
            }
        }

        /// <summary>
        /// Fordert den Benutzer auf, eine ganze Zahl innerhalb eines bestimmten Bereichs einzugeben und gibt einen Standardwert zurück, wenn keine Eingabe erfolgt.
        /// </summary>
        /// <param name="prompt">Die Eingabeaufforderung, die dem Benutzer angezeigt wird.</param>
        /// <param name="min">Der minimale zulässige Wert (optional, Standard: int.MinValue).</param>
        /// <param name="max">Der maximale zulässige Wert (optional, Standard: int.MaxValue).</param>
        /// <param name="defaultValue">Der Standardwert, falls keine gültige Eingabe erfolgt (optional, Standard: 0).</param>
        /// <returns>Die eingegebene ganze Zahl oder der Standardwert, falls die Eingabe ungültig ist.</returns>
        /// <exception cref="ArgumentException">Wird geworfen, wenn der Mindestwert größer als der Höchstwert ist.</exception>
        public static int GetIntInputBetweenWithDefaultValue(string prompt, int min = int.MinValue, int max = int.MaxValue, int defaultValue = 0)
        {
            if (min > max)
            {
                throw new ArgumentException("Minimum value cannot be greater than maximum value.");
            }

            int result;
            while (true)
            {
                Console.Write(prompt);

                string input = GetStringInput(prompt);

                if(string.IsNullOrEmpty(input))
                {
                    return defaultValue;
                }

                if (int.TryParse(input, out result) && result >= min && result <= max)
                {
                    break;
                }
                Console.WriteLine($"Invalid input. Please enter an integer between {min} and {max}.");
            }
            return result;
        }

        /// <summary>
        /// Fordert den Benutzer auf, ein Datum einzugeben, und gibt das eingegebene Datum zurück.
        /// </summary>
        /// <param name="prompt">Die Eingabeaufforderung, die dem Benutzer angezeigt wird.</param>
        /// <returns>Das eingegebene Datum als DateTime-Objekt oder null, wenn keine Eingabe erfolgt.</returns>
        public static DateTime? GetDateTimeInput(string prompt)
        {
            DateTime result;
            while (true)
            {
                string birthdateInput = GetStringInput(prompt);

                if (string.IsNullOrEmpty(birthdateInput))
                {
                    return null;
                }

                if (DateTime.TryParse(birthdateInput, out result))
                {
                    break;
                }

                Console.WriteLine($"Invalid date format. Please enter in {DateFormat} format.");
            }

            return result;
        }

        /// <summary>
        /// Fordert den Benutzer auf, ein Datum einzugeben, und gibt das eingegebene Datum oder einen Standardwert zurück.
        /// </summary>
        /// <param name="prompt">Die Eingabeaufforderung, die dem Benutzer angezeigt wird.</param>
        /// <param name="defaultValue">Der Standardwert, der zurückgegeben wird, wenn keine gültige Eingabe erfolgt (optional).</param>
        /// <returns>Das eingegebene Datum als DateTime-Objekt oder den Standardwert, wenn keine gültige Eingabe erfolgt oder der Benutzer keine Eingabe macht.</returns>
        public static DateTime? GetDateTimeInputWithDefaultValue(string prompt, DateTime? defaultValue = null)
        {
            DateTime result;
            while (true)
            {
                string birthdateInput = GetStringInputWithDefaultValue(GetStringTrimInput(prompt), defaultValue?.ToString(DateFormat));

                if (string.IsNullOrEmpty(birthdateInput))
                {
                    return defaultValue;
                }

                if (!string.IsNullOrEmpty(birthdateInput) && DateTime.TryParse(birthdateInput, out result))
                {
                    break;
                }

                Console.WriteLine($"Invalid date format. Please enter in {DateFormat} format.");
            }

            return result;
        }

        // <summary>
        /// Prompts the user to enter a comma-separated list of IDs and retrieves the corresponding unique entities from a provided collection.
        /// </summary>
        /// <typeparam name="T">The type of the entities in the `availableEntities` collection. 
        /// This type must implement the `class` and `IIdentifiable<int>` interfaces. 
        /// The `IIdentifiable<int>` interface requires the entity to have an `Id` property of type `int` for identification.</typeparam>
        /// <param name="availableEntities">A collection containing all available entities to choose from.</param>
        /// <param name="prompt">The message displayed to the user requesting input.</param>
        /// <returns>A collection containing the unique selected entities based on the parsed IDs from the user's input. 
        /// An empty collection is returned if no valid input is provided.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the `availableEntities` collection is null.</exception>
        public static ICollection<T> GetStringInputs<T>(ICollection<T> availableEntities, string prompt) where T : class, IIdentifiable<int>
        {
            if (availableEntities == null)
            {
                throw new ArgumentNullException(nameof(availableEntities));
            }

            List<T> selectedEntities = new();

            string studentIdsInput = GetStringTrimInput($"\n{prompt}");
            if (string.IsNullOrEmpty(studentIdsInput))
            {
                return selectedEntities; // Early return if no input
            }

            foreach (string idString in studentIdsInput.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(idString, out int id))
                {
                    T? entity = availableEntities.FirstOrDefault(e => e.Id == id);
                    if (entity != null)
                    {
                        selectedEntities.Add(entity);
                    }
                }
            }
            return selectedEntities.Distinct().ToList();
        }
    }
}
