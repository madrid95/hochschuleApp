using System;
namespace HochschuleApp.exceptions
{
    public static class ExceptionHandler
    {
        public static void Invoke(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static T? Invoke<T>(Func<T> function) where T : class
        {
            try
            {
                return function.Invoke();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                {
                    return Activator.CreateInstance<T>() as T;
                }
                return default;
            }
        }
    }
}
