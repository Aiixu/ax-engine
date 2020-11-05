using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Contains information about the console history.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct CONSOLE_HISTORY_INFO
        {
            /// <summary>
            ///  The size of the structure, in bytes.
            /// </summary>
            public ushort cbSize;

            /// <summary>
            ///  The number of commands kept in each history buffer.
            /// </summary>
            public ushort HistoryBufferSize;

            /// <summary>
            ///  The number of history buffers kept for this console process.
            /// </summary>
            public ushort NumberOfHistoryBuffers;

            /// <summary>
            ///  This parameter can be zero or the following value.
            /// </summary>
            public uint dwFlags;
        }
    }
}
