using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_SELECTION_INFO
        {
            public uint Flags;
            public COORD SelectionAnchor;
            public SMALL_RECT Selection;
        }
    }
}
