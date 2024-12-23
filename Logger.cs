using System;
using System.IO;

namespace HG
{
    public static class Logger
    {
        private static readonly string LogFilePath = "log.txt";

        // Метод для записи сообщения в лог
        public static void Log(string message)
        {

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logMessage = $"{timestamp}: {message}";
            File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);

        }
    }
}
