using NuGet.Common;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Chorizite.PluginIndexBuilder {
    internal class Logger : ILogger {
        private string name;

        public Logger(string name) {
            this.name = name;
        }

        public void Log(LogLevel level, string data) {
            Console.WriteLine($"{name}[{level}]: {data}");
        }

        public void Log(ILogMessage message) {
            Console.WriteLine($"{name}: {message.Message}");
        }

        public Task LogAsync(LogLevel level, string data) {
            Console.WriteLine($"{name}[{level}]: {data}");
            return Task.CompletedTask;
        }

        public Task LogAsync(ILogMessage message) {
            Console.WriteLine($"{name}: {message.Message}");
            return Task.CompletedTask;
        }

        public void LogDebug(string data) {
            Log(LogLevel.Debug, data);
        }

        public void LogError(string data) {
            Log(LogLevel.Error, data);
        }

        public void LogInformation(string data) {
            Log(LogLevel.Information, data);
        }

        public void LogInformationSummary(string data) {
            Log(LogLevel.Information, data);
        }

        public void LogMinimal(string data) {
            Log(LogLevel.Minimal, data);
        }

        public void LogVerbose(string data) {
            Log(LogLevel.Verbose, data);
        }

        public void LogWarning(string data) {
            throw new System.NotImplementedException();
        }
    }
}