using System;
using System.Diagnostics;
using System.IO;

namespace WinkingCat.HelperLibs
{
    public static class Logger
    {
        public static string logFile { get; set; }
        public static string messageFormat { get; set; } = "{0:yyyy-MM-dd HH:mm:ss.fff} - {1}";

        public static void Init(string fileName)
        {
            logFile = fileName;
            DirectoryManager.CreateDirectoryFromFilePath(fileName);
        }

        public static void WriteLine(string message = "")
        {
            if (logFile != null)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    message = string.Format(messageFormat, DateTime.Now, message);
                    File.AppendAllText(logFile, message+Environment.NewLine);
                    Console.WriteLine(message);
                }
            }
            else
            {
                Debug.WriteLine(message);
            }
        }

        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        public static void WriteException(string exception, string message = "Exception")
        {
            WriteLine($"{message}:{Environment.NewLine}{exception}");
            Console.WriteLine(exception);
        }

        public static void WriteException(Exception exception, string message = "Exception")
        {
            WriteException(exception.ToString(), message);
        }
    }
}
