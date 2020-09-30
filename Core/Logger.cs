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
            int[] colWidthes = new int[args.GetLength(0)];

            for (int y = 0; y < args.GetLength(1); y++)
            {
                for (int x = 0; x < args.GetLength(0); x++)
                {
                    if(args[x, y] == null) { continue; }
                    colWidthes[x] = Math.Min(maxColWidth, Math.Max(colWidthes[x], args[x, y].ToString().Length));
                }
            }

            IEnumerable<int> xIterator = Enumerable.Range(0, colWidthes.Length);
            string horizontalSeparator = $"+{string.Join("", xIterator.Select(x => $"{new string('-', colWidthes[x])}+"))}";

            string GetRow(int y) => $"|{string.Join("|", xIterator.Select(x => $"{args[x, y]}{new string(' ', colWidthes[x] - (args[x, y]?.ToString().Length ?? 0))}"))}|";

            StringBuilder table = new StringBuilder()
                /* HEADER */ .Append($"{horizontalSeparator}\n{GetRow(0)}\n{horizontalSeparator}")       
                /* ROWS   */ .Append($"\n{string.Join("\n", Enumerable.Range(1, args.GetLength(1) - 1).Select(y => GetRow(y)))}")
                /* FOOTER */ .Append($"\n{horizontalSeparator}");

            return table.ToString();
        }
    }
}

// disable auto return ???????