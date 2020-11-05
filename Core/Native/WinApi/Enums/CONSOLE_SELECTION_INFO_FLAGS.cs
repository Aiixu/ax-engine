namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  The selection indicator.
        /// </summary>
        public enum CONSOLE_SELECTION_INFO_FLAGS : uint
        {
            /// <summary>
            ///  Mouse is down.
            /// </summary>
            CONSOLE_MOUSE_DOWN = 0x0008,

            /// <summary>
            ///  Selecting with the mouse.
            /// </summary>
            CONSOLE_MOUSE_SELECTION = 0x0004,

            /// <summary>
            ///  No selection.
            /// </summary>
            CONSOLE_NO_SELECTION = 0x0000,

            /// <summary>
            ///  Selection has begun.
            /// </summary>
            CONSOLE_SELECTION_IN_PROGRESS = 0x0001,

            /// <summary>
            ///  Selection rectangle is not empty.
            ///  /// </summary>
            CONSOLE_SELECTION_NOT_EMPTY = 0x0002
        }
    }
}
