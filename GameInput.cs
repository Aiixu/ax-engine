using System;
using System.Collections.Generic;

using Ax.Engine.Core;
using Ax.Engine.Utils;

using static Ax.Engine.Core.Native;
using static Ax.Engine.Core.InputHandler;

namespace Ax.Engine
{
    public static class GameInput
    {
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

        public static void RegisterAxis(Axis axis, bool overrideIfExists = false)
        {
            if (overrideIfExists || !axises.ContainsKey(axis.name))
            {
                axises[axis.name] = axis;
            }
        }

        public static Vector2 GetMousePosition()
        {
            return Vector2.Zero;
        }

        public static bool GetMouseButtonDown(MOUSE_BUTTON button)
        {
            return false;
        }

        public static bool GetMouseButton(MOUSE_BUTTON button)
        {
            return false;
        }

        public static bool GetMouseButtonUp(MOUSE_BUTTON button)
        {
            return false;
        }

        public static int GetMouseWheelHorizontal()
        {
            return currentMouseButtonStates.ContainsValue((uint)MOUSE_EVENT_FLAGS.MOUSE_WHEELED) ?
                currentMouseButtonStates.ContainsKey((uint)MOUSE_BUTTON.WHEEL_DOWN) ? -1 : 1 : 0;
        }
         
        public static void RegisterKeyEvent(KeyEventType keyEventType, Action callback, params KEY[] keys)
        {
            RegisterKeyEvent(keyEventType, callback, false, keys); ;
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

        public static int GetAxis(string axisName)
        {
            return 0;
        }

        public enum KeyEventType : int
        {
            KeyDown = 0,
            KeyPress = 1,
            KeyUp = 2
        }

        public sealed class Axis
        {
            public string name;

            public KEY positiveKey;
            public KEY negativeKey;

            public KEY alternativePositiveKey;
            public KEY alternativeNegativeKey;
        }   

        public sealed class Button
        {
            public string name;

            public KEY key;
        }
    }
}
