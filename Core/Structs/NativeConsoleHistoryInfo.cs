using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_HISTORY_INFO
        {
            public ushort cbSize;
            public ushort HistoryBufferSize;
            public ushort NumberOfHistoryBuffers;
            public uint dwFlags;
        }
    }
}
