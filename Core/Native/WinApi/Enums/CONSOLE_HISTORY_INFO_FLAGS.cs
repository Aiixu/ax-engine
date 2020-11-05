namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Flags for <see cref="CONSOLE_HISTORY_INFO.dwFlags"/>.
        /// </summary>
        public enum CONSOLE_HISTORY_INFO_FLAGS : uint
        {
            /// <summary>
            ///  Duplicate entries will not be stored in the history buffer.
            /// </summary>
            HISTORY_NO_DUP_FLAG = 0x1
        }
    }
}
