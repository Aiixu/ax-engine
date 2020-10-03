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

        private static Func<char, bool>[] keyEventsCheck = new Func<char, bool>[] { GetKeyDown, GetKey, GetKeyUp };
        private static Dictionary<char, EventHandler>[] keyEvents = new Dictionary<char, EventHandler>[3]
        {
            new Dictionary<char, EventHandler>(),
            new Dictionary<char, EventHandler>(),
            new Dictionary<char, EventHandler>()
        };

        public enum KeyEventType : int
        {
            KeyDown = 0,
            KeyPress = 1,
            KeyUp = 2
        }

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

        #region Mouse Input
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
        #endregion

        #region Keyboard Input
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
        #endregion

        #region Keyboard Input Events
        public static void RegisterKeyEvent(KeyEventType keyEventType, Action callback, params KEY[] keys)
        {
            RegisterKeyEvent(keyEventType, callback, false, keys);;
        }

        public static void RegisterKeyEvent(KeyEventType keyEventType, Action callback, bool caseSensitive, params KEY[] keys)
        {
            List<char> chKeys = new List<char>();

            for (int i = 0; i < keys.Length; i++)
            {
                char chKey = (char)keys[i];
                if (caseSensitive)
                {
                    chKeys.Add(chKey);
                }
                else
                {
                    chKeys.Add(char.ToLower(chKey));
                    chKeys.Add(char.ToUpper(chKey));
                }
            }

            RegisterKeyEvent(keyEventType, callback, chKeys.ToArray());
        }

        public static void RegisterKeyEvent(KeyEventType keyEventType, Action callback, params char[] keys)
        {
            int registryIndex = (int)keyEventType;

            for (int i = 0; i < keys.Length; i++)
            {
                if (!keyEvents[registryIndex].ContainsKey(keys[i]))
                {
                    keyEvents[registryIndex].Add(keys[i], null);
                }

                keyEvents[registryIndex][keys[i]] += (object sender, EventArgs e) => callback();
            }
        }
        #endregion

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
