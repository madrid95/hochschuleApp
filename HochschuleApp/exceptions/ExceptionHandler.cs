using System;
using HochschuleApp.logger;

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
                ConsoleLogger.LogError(ex);
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
                ConsoleLogger.LogError(ex);
                if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                {
                    return Activator.CreateInstance<T>() as T;
                }
                return default;
            }
        }
    }
}
