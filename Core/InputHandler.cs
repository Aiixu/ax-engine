using System;
using System.Text;
using System.Collections.Generic;

using static Ax.Engine.Core.Native;

namespace Ax.Engine.Core
{
    public sealed class InputHandler
    {
        public IntPtr Handle { get => handle; }

        private IntPtr handle;
        private CONSOLE_MODE_INPUT inLast;

        internal static readonly Dictionary<string, GameInput.Axis> axises = new Dictionary<string, GameInput.Axis>();

        internal static Dictionary<char, bool> lastKeyStates = new Dictionary<char, bool>();
        internal static Dictionary<char, bool> currentKeyStates = new Dictionary<char, bool>();

        internal static Dictionary<uint, uint> lastMouseButtonStates = new Dictionary<uint, uint>();
        internal static Dictionary<uint, uint> currentMouseButtonStates = new Dictionary<uint, uint>();

        internal static Func<char, bool>[] keyEventsCheck = new Func<char, bool>[] { GameInput.GetKeyDown, GameInput.GetKey, GameInput.GetKeyUp };
        internal static Dictionary<char, EventHandler>[] keyEvents = new Dictionary<char, EventHandler>[3]
        {
            new Dictionary<char, EventHandler>(),   // Key down
            new Dictionary<char, EventHandler>(),   // Key press
            new Dictionary<char, EventHandler>()    // Key up
        };

        private static Func<uint, bool>[] mouseButtonEventsCheck = new Func<uint, bool>[] { };
        private static Dictionary<uint, EventHandler>[] mouseButtonsEvents = new Dictionary<uint, EventHandler>[7]
        {
            new Dictionary<uint, EventHandler>(),   // Mouse down
            new Dictionary<uint, EventHandler>(),   // Mouse press
            new Dictionary<uint, EventHandler>(),   // Mouse up
            new Dictionary<uint, EventHandler>(),   // Mouse double click
            new Dictionary<uint, EventHandler>(),   // Mouse horizontal move
            new Dictionary<uint, EventHandler>(),   // Mouse vertical move
            new Dictionary<uint, EventHandler>()    // Mouse moved
        };

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
            lastMouseButtonStates = new Dictionary<uint, uint>(currentMouseButtonStates);

            currentKeyStates.Clear();
            currentMouseButtonStates.Clear();

            for (int i = 0; i < rec.Length; i++)
            {
                switch(rec[i].EventType)
                {
                    case (ushort)INPUT_RECORD_EVENT_TYPE.KEY_EVENT:
                        currentKeyStates[rec[i].KeyEvent.UnicodeChar] = rec[i].KeyEvent.bKeyDown;
                        break;

                    case (ushort)INPUT_RECORD_EVENT_TYPE.MOUSE_EVENT:
                        Console.WriteLine(rec[i].MouseEvent.dwButtonState + " " + rec[i].MouseEvent.dwEventFlags);
                        currentMouseButtonStates[rec[i].MouseEvent.dwButtonState] = rec[i].MouseEvent.dwEventFlags;
                        break;
                }
            }

            for (int i = 0; i < keyEvents.Length; i++)
            {
                foreach (KeyValuePair<char, EventHandler> keyEvent in keyEvents[i])
                {
                    if (keyEventsCheck[i].Invoke(keyEvent.Key))
                    {
                        keyEvent.Value.Invoke(null, null);
                    }
                }
            }

            return;

            Console.WriteLine("-----");
            foreach (var item in currentMouseButtonStates)
            {
                Console.WriteLine($"{item.Key} -- {item.Value}");
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
    }
}
