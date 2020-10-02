using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Describes an input event in the console input buffer. These records can be read from the input buffer by using the <see cref="ReadConsoleInput(System.IntPtr, INPUT_RECORD[], uint, out uint)" or <see cref="PeekConsoleInput(System.IntPtr, INPUT_RECORD[], uint, out uint)"/> function, or written to the input buffer by using the <see cref="WriteConsoleInput(System.IntPtr, INPUT_RECORD[], uint, out uint)"/> function.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT_RECORD
        {
            /// <summary>
            ///  A handle to the type of input event and the event record stored in the Event member.
            /// </summary>
            [FieldOffset(0)] public ushort EventType;

            /// <summary>
            ///  Contains informations about a keyboard input event.
            /// </summary>
            [FieldOffset(4)] public KEY_EVENT_RECORD KeyEvent;

            /// <summary>
            ///  Contains informations about a mouse input event.
            /// </summary>
            [FieldOffset(4)] public MOUSE_EVENT_RECORD MouseEvent;

            /// <summary>
            ///  Contains informations about a change in the size of the console screen buffer.
            /// </summary>
            [FieldOffset(4)] public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;

            /// <summary>
            ///  Contains informations about a menu input event.
            /// </summary>
            [FieldOffset(4)] public MENU_EVENT_RECORD MenuEvent;

            /// <summary>
            ///  Contains informations about a focus input event.
            /// </summary>
            [FieldOffset(4)] public FOCUS_EVENT_RECORD FocusEvent;
        }
    }
}
