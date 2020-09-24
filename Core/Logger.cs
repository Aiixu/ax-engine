using System;
using System.IO;

namespace Ax.Engine.Core
{
    public static class Logger
    {
        public static string DebugFolderPath { get; internal set; }
        public static string CurrentDebugFile { get; private set; }

        internal static void CreateNewDebugFile()
        {
            if (string.IsNullOrEmpty(DebugFolderPath)) { DebugFolderPath = "logs"; }
            if (!Directory.Exists(DebugFolderPath)) { Directory.CreateDirectory(DebugFolderPath); }

            DateTime now = DateTime.Now;

            CurrentDebugFile = Path.Combine(DebugFolderPath, string.Concat(now.GetHashCode(), ".txt"));
            File.WriteAllText(CurrentDebugFile, string.Concat("Debug file: ", now.ToString()));
        }

        public static void Write(string message)
        {
            if (string.IsNullOrEmpty(CurrentDebugFile)) { CreateNewDebugFile(); }

            File.AppendAllText(CurrentDebugFile, string.Concat("\n", message));
        }
    }
}
