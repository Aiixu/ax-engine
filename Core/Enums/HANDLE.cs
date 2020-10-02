namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Represents the different handles for the standard output, input and error device.
        /// </summary>
        public enum HANDLE : uint
        {
            /// <summary>
            ///  The standard input device.
            /// </summary>
            STD_INPUT_HANDLE = unchecked((uint)-11),

            /// <summary>
            ///  The standard output device.
            /// </summary>
            STD_OUTPUT_HANDLE = unchecked((uint)-10),

            /// <summary>
            ///  The standard error device.
            /// </summary>
            STD_ERROR_HANDLE = unchecked((uint)-12)
        }
    }
}
