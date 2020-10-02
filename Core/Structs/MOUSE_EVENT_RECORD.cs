using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Describes a mouse input event in a console <see cref="INPUT_RECORD"/> structure.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSE_EVENT_RECORD
        {
            /// <summary>
            ///  A <see cref="COORD"/> structure that contains the location of the cursor, in terms of the console screen buffer's character-cell coordinates.
            /// </summary>
            public COORD dwMousePosition;

            /// <summary>
            ///  The status of the mouse buttons. The least significant bit corresponds to the leftmost mouse button. The next least significant bit corresponds to the rightmost mouse button. The next bit indicates the next-to-leftmost mouse button. The bits then correspond left to right to the mouse buttons. A bit is 1 if the button was pressed. See <see cref="MOUSE_BUTTON_STATE"/>
            /// </summary>
            public uint dwButtonState;

            /// <summary>
            ///  The state of the control keys. This member can be one or more of the following values. See <see cref="CONTROL_KEY_STATE"/>
            /// </summary>
            public uint dwControlKeyState;

            /// <summary>
            ///  The type of mouse event. If this value is zero, it indicates a mouse button being pressed or released. <see cref="MOUSE_EVENT_FLAGS"/>
            /// </summary>
            public uint dwEventFlags;
        }
    }
}
