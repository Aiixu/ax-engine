using System;
using System.Text;
using System.Collections.Generic;

using Ax.Engine.Utils;
using static Ax.Engine.Core.Native;

namespace Ax.Engine.Core
{
    public sealed class InputHandler
    {
        public const short KEY_SCREENSHOT = (short)KEY.F2;

        public IntPtr Handle { get => handle; }

        private IntPtr handle;
        private CONSOLE_MODE_INPUT inLast;

        private static readonly Dictionary<string, Axis> axises = new Dictionary<string, Axis>();

        private static Dictionary<char, bool> lastKeyStates = new Dictionary<char, bool>();
        private static Dictionary<char, bool> currentKeyStates = new Dictionary<char, bool>();

        private static Dictionary<char, Action> keyEvents = new Dictionary<char, Action>();

        public bool Enable(ref StringBuilder logger)
        {
            bool stdIn;
            logger.AppendLine($"GETSTDIN       {stdIn = GetStdIn(out handle)}");

            if (!stdIn) { return false; }
            if (!GetConsoleModeIn(Handle, ref logger, out inLast)) { return false; }

            CONSOLE_MODE_INPUT mode = inLast | CONSOLE_MODE_INPUT.ENABLE_VIRTUAL_TERMINAL_INPUT;

            logger.AppendLine($"INLAST         {inLast}");
            logger.AppendLine($"INMODE         {mode}");

            bool cMode;
            logger.AppendLine($"SETCMODEIN     {cMode = SetConsoleMode(Handle, (uint)mode)}");

            return cMode;
        }

        public bool Disable()
        {
            handle = IntPtr.Zero;
            return GetStdIn(out IntPtr hIn) && SetConsoleMode(hIn, (uint)inLast);
        }
        
        public uint Read(out INPUT_RECORD[] rec)
        {
            GetNumberOfConsoleInputEvents(Handle, out uint numberOfEvents);

            rec = new INPUT_RECORD[numberOfEvents];
            ReadConsoleInput(Handle, rec, numberOfEvents, out uint numberOfEventRead);

            return numberOfEventRead;
        }

        public uint Peek(out INPUT_RECORD[] rec)
        {
            GetNumberOfConsoleInputEvents(Handle, out uint numberOfEvents);

            rec = new INPUT_RECORD[numberOfEvents];
            PeekConsoleInput(Handle, rec, numberOfEvents, out uint numberOfEventRead);

            return numberOfEventRead;
        }

        internal void UpdateInputStates(INPUT_RECORD[] rec)
        {
            lastKeyStates = new Dictionary<char, bool>(currentKeyStates);
            currentKeyStates.Clear();

            for (int i = 0; i < rec.Length; i++)
            {
                switch(rec[i].EventType)
                {
                    case (ushort)INPUT_RECORD_EVENT_TYPE.KEY_EVENT:
                        currentKeyStates[rec[i].KeyEvent.UnicodeChar] = rec[i].KeyEvent.bKeyDown;
                        break;
                }
            }
        }

        private bool GetStdIn(out IntPtr handle)
        {
            handle = GetStdHandle((uint)HANDLE.STD_INPUT_HANDLE);
            return handle != INVALID_HANDLE;
        }

        private bool GetConsoleModeIn(IntPtr hConsoleHandle, ref StringBuilder logger, out CONSOLE_MODE_INPUT mode)
        {
            bool cMode;
            logger.AppendLine($"GETCMODEIN     {cMode = GetConsoleMode(hConsoleHandle, out uint lpMode)}");

            if (!cMode)
            {
                mode = 0;
                return false;
            }

            mode = (CONSOLE_MODE_INPUT)lpMode;
            return true;
        }

        public static void RegisterAxis(Axis axis, bool overrideIfExists = false)
        {
            if(overrideIfExists || !axises.ContainsKey(axis.name))
            {
                axises[axis.name] = axis;
            }
        }

        public static Vector2 GetMousePosition()
        {
            return Vector2.Zero;
        }

        public static bool GetMouseButtonDown(MOUSE_BUTTON_STATE button)
        {
            return false;
        }

        public static bool GetMouseButton(MOUSE_BUTTON_STATE button)
        {
            return false;
        }

        public static bool GetMouseButtonUp(MOUSE_BUTTON_STATE button)
        {
            return false;
        }

        public static bool GetKeyDown(KEY key, bool caseSensitive = false)
        {
            char chKey = (char)key;
            return caseSensitive ? GetKeyDown(chKey) : GetKeyDown(char.ToUpper(chKey)) || GetKeyDown(char.ToLower(chKey));
        }

        public static bool GetKeyDown(char chKey)
        {
            return (!lastKeyStates.ContainsKey(chKey) || !lastKeyStates[chKey]) && GetKey(chKey);
        }

        public static bool GetKey(KEY key)
        {
            char chKey = (char)key;
            return GetKey(chKey);
        }

        public static bool GetKey(char chKey)
        {
            return currentKeyStates.ContainsKey(chKey) && currentKeyStates[chKey];
        }

        public static bool GetKeyUp(KEY key)
        {
            char chKey = (char)key;
            return GetKeyUp(chKey);
        }

        public static bool GetKeyUp(char chKey)
        {
            return lastKeyStates.ContainsKey(chKey) && lastKeyStates[chKey] && !GetKey(chKey);
        }

        public static int GetAxis(string axisName)
        {
            return 0;
        }

        public sealed class Axis
        {
            public string name;

            public KEY positiveKey;
            public KEY negativeKey;

            public KEY alternativePositiveKey;
            public KEY alternativeNegativeKey;
        }
    }
}
