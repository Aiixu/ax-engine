using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            CurrentDebugFile = Path.Combine(DebugFolderPath, string.Concat(now.ToString("yyyy-MM-dd_HH-mm-ss-ff"), ".txt"));
            File.WriteAllText(CurrentDebugFile, string.Concat("Debug file: ", now.ToString()));
        }

        public static void Write(object message) => Write(message.ToString());
        public static void Write(string message)
        {
            if (string.IsNullOrEmpty(CurrentDebugFile)) { CreateNewDebugFile(); }

            File.AppendAllText(CurrentDebugFile, string.Concat("\n", message));
        }

        public static string GenTable(object[,] args, int maxColWidth = int.MaxValue)
        {
            int[] colWidthes = new int[args.GetLength(1)];

            for (int x = 0; x < args.GetLength(1); x++)
            {
                for (int y = 0; y < args.GetLength(0); y++)
                {
                    colWidthes[x] = Math.Min(maxColWidth, Math.Max(colWidthes[x], args[y, x].ToString().Length));
                }
            }

            IEnumerable<int> xIterator = Enumerable.Range(0, colWidthes.Length);

            string horizontalSeparator = $"+{string.Join("", xIterator.Select(x => $"{new string('-', colWidthes[x])}+"))}";

            StringBuilder table 
                = new StringBuilder()
                .Append($"{horizontalSeparator}\n{$"|{string.Join("|", xIterator.Select(x => $"{args[0, x]}{new string(' ', colWidthes[x] - args[0, x].ToString().Length)}"))}|"}\n{horizontalSeparator}")
                .Append($"\n{string.Join("\n", Enumerable.Range(1, args.GetLength(0) - 1).Select(y => $"|{string.Join("|", xIterator.Select(x => $"{args[y, x]}{new string(' ', colWidthes[x] - args[y, x].ToString().Length)}"))}|"))}")
                .Append($"\n{horizontalSeparator}");

            return table.ToString();
        }
    }
}

// disable auto return ???????