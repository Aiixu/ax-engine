namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Contains information for a console read operation.
        /// </summary>
        public struct CONSOLE_READCONSOLE_CONTROL
        {
            /// <summary>
            ///  The size of the structure.
            /// </summary>
            public ulong nLength;

            /// <summary>
            ///  The number of characters to skip (and thus preserve) before writing newly read input in the buffer passed to the <see cref="ReadConsole(System.IntPtr, System.Text.StringBuilder, uint, out uint, System.IntPtr)"/> function. This value must be less than the <i>nNumberOfCharsToRead</i> parameter of the <see cref="ReadConsole(System.IntPtr, System.Text.StringBuilder, uint, out uint, System.IntPtr)"/> function.
            /// </summary>
            public ulong nInitialChars;

            /// <summary>
            ///  A user-defined control character used to signal that the read is complete.
            /// </summary>
            public ulong dwCtrlWakeupMask;

            /// <summary>
            ///  The state of the control keys. See <see cref="CONTROL_KEY_STATE"/>.
            /// </summary>
            public ulong dwControlKeyState;
        }
    }
}
