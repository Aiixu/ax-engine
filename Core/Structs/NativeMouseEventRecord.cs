using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSE_EVENT_RECORD
        {
            public COORD dwMousePosition;

            public uint dwButtonState;
            public uint dwControlKeyState;
            public uint dwEventFlags;
        }
    }
}
