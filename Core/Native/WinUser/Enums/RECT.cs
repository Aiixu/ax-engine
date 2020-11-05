using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinUser
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
