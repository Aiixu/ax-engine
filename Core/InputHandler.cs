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

        internal static Dictionary<uint, List<uint>> lastRecordedMouseButtonStates = new Dictionary<uint, List<uint>>();

        //internal static Dictionary<uint, uint> lastMouseButtonStates = new Dictionary<uint, uint>();
        internal static Dictionary<uint, List<uint>> currentMouseButtonStates = new Dictionary<uint, List<uint>>();

        public bool Enable()
        {
            if (!GetStdIn(out handle)) { return false; }
            if (!GetConsoleModeIn(Handle, out inLast)) { return false; }

            CONSOLE_MODE_INPUT mode = inLast | CONSOLE_MODE_INPUT.ENABLE_VIRTUAL_TERMINAL_INPUT;

            return SetConsoleMode(Handle, (uint)mode);
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
            //lastMouseButtonStates = new Dictionary<uint, List<uint>>(currentMouseButtonStates);

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
                        if(!currentMouseButtonStates.ContainsKey(rec[i].MouseEvent.dwButtonState))
                        {
                            currentMouseButtonStates[rec[i].MouseEvent.dwButtonState] = new List<uint>();
                        }

                        currentMouseButtonStates[rec[i].MouseEvent.dwButtonState].Add(rec[i].MouseEvent.dwEventFlags);
                        break;
                }
            }

            return;
        }

        private bool GetStdIn(out IntPtr handle)
        {
            handle = GetStdHandle((uint)HANDLE.STD_INPUT_HANDLE);
            return handle != INVALID_HANDLE;
        }

        private bool GetConsoleModeIn(IntPtr hConsoleHandle, out CONSOLE_MODE_INPUT mode)
        {
            if (!GetConsoleMode(hConsoleHandle, out uint lpMode))
            {
                mode = 0;
                return false;
            }

            mode = (CONSOLE_MODE_INPUT)lpMode;
            return true;
        }
    }
}
