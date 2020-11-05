namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Indicates how to define the access to the console buffer.
        /// </summary>
        public enum BUFFER_ACCESS_MODE : long
        {
            /// <summary>
            ///  Requests read access to the console screen buffer, enabling the process to read data from the buffer.
            /// </summary>
            GENERIC_READ = 0x80000000L,

            /// <summary>
            ///  Requests write access to the console screen buffer, enabling the process to write data to the buffer.
            /// </summary>
            GENERIC_WRITE = 0x40000000L
        }
    }
}
