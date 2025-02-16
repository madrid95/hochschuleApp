using System;
namespace HochschuleApp.logger
{
	public interface ILogger
	{
        void LogError(Exception exception);
        void LogInfo(string message);
    }
}

