using System;
using MyApp.Application.Interfaces.Services;

namespace MyApp.Test.Infrastructure
{
    public class FakeLoggerService : ILoggerService
    {
        public void LogError(string errorMessage)
        {
        }

        public void LogError(string errorMessage, params object[] args)
        {
        }

        public void LogException(Exception ex)
        {
        }

        public void LogInfo(string infoMessage)
        {
        }

        public void LogInfo(string infoMessage, params object[] args)
        {
        }
    }
}