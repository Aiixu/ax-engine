namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Indicates how to define the access to the console buffer.
        /// </summary>
        public enum BUFFER_SHARE_MODE : long
        {
            /// <summary>
            ///  Don't share.
            /// </summary>
            FILE_DONT_SHARE = 0,

            /// <summary>
            ///  Other open operations can be performed on the console screen buffer for read access.
            /// </summary>
            FILE_SHARE_READ = 0x00000001,

            /// <summary>
            ///  Other open operations can be performed on the console screen buffer for write access.
            /// </summary>
            FILE_SHARE_WRITE = 0x00000001
        }
    }
}
