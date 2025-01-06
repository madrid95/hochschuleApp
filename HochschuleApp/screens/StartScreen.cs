using System;
namespace HochschuleApp.screens
{
    /// <summary>
    /// Klasse für die Startbildschirm-Logik.
    /// </summary>
    public static class StartScreen
	{
        /// <summary>
        /// Zeigt den Startbildschirm an und fragt den Benutzer nach der gewünschten Datenbank.
        /// </summary>
        /// <returns>
        /// Gibt `true` zurück, wenn die In-Memory-Datenbank ausgewählt wurde, 
        /// andernfalls `false`.
        /// </returns>
        public static bool Start()
		{
            Console.WriteLine("Choose your database:");
            Console.WriteLine("1. In-Memory Database");
            Console.WriteLine("2. MySQL");

            int choice = InputScreen.GetIntInputBetween("Please enter the value: ", 1, 2);

            return choice == 1;
        }
    }
}
