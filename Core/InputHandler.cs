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

        private static Dictionary<ushort, bool> LAST_KEY_STATES = new Dictionary<ushort, bool>();
        private static Dictionary<ushort, bool> CURRENT_KEY_STATES = new Dictionary<ushort, bool>();

        private static List<KEY_EVENT_RECORD> LAST_KEY_EVENTS = new List<KEY_EVENT_RECORD>();
        private static List<MOUSE_EVENT_RECORD> LAST_MOUSE_EVENTS = new List<MOUSE_EVENT_RECORD>();

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

        internal void UpdateEventRegistry(INPUT_RECORD[] rec)
        {
            LAST_KEY_STATES = new Dictionary<ushort, bool>(CURRENT_KEY_STATES);
            CURRENT_KEY_STATES.Clear();

            for (int i = 0; i < rec.Length; i++)
            {
                switch(rec[i].EventType)
                {
                    case (ushort)INPUT_RECORD_EVENT_TYPE.KEY_EVENT:
                        CURRENT_KEY_STATES[rec[i].KeyEvent.wVirtualKeyCode] = rec[i].KeyEvent.bKeyDown;
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

        public static bool GetKeyDown(KEY key)
        {
            ushort vkKey = (ushort)key;
            return (!LAST_KEY_STATES.ContainsKey(vkKey) || !LAST_KEY_STATES[vkKey]) && GetKey(key);
        }

        public static bool GetKey(KEY key)
        {
            ushort vkKey = (ushort)key;
            return CURRENT_KEY_STATES.ContainsKey(vkKey) && CURRENT_KEY_STATES[vkKey];
        }

        public static bool GetKeyUp(KEY key)
        {
            ushort vkKey = (ushort)key;
            return LAST_KEY_STATES.ContainsKey(vkKey) && LAST_KEY_STATES[vkKey] && !GetKey(key);
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
