namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Describe different types of CTRL signals.
        /// </summary>
        public enum CTRL_EVENT
        {
            /// <summary>
            ///  Generates a CTRL+C signal. This signal cannot be generated for process groups. If dwProcessGroupId is nonzero, this function will succeed, but the CTRL+C signal will not be received by processes within the specified process group.
            /// </summary>
            CTRL_C_EVENT = 0,

            /// <summary>
            ///  Generates a CTRL+BREAK signal.
            /// </summary>
            CTRL_BREAK_EVENT = 1,

            /// <summary>
            ///  Generates a shutdown signal.
            /// </summary>
            CTRL_SHUTDOWN_EVENT = 6
        }
    }
}
