namespace Ax.Engine.Core.Native
{
    public static partial class WinUser
    {
        /// <summary>
        ///  The type of system command requested.
        /// </summary>
        public enum SC : uint
        {
            /// <summary>
            ///  Closes the window.
            /// </summary>
            CLOSE = 0xF060,

            /// <summary>
            ///  Changes the cursor to a question mark with a pointer.
            /// </summary>
            CONTEXTHELP = 0xF180,

            /// <summary>
            ///  Selects the default item; the user double-clicked the window menu.
            /// </summary>
            DEFAULT = 0xF160,

            /// <summary>
            ///  Activates the window associated with the application-specified hot key. The lParam parameter identifies the window to activate.
            /// </summary>
            HOTKEY = 0xF150,

            /// <summary>
            ///  Scrolls horizontally.
            /// </summary>
            HSSCROLL = 0xF080,

            /// <summary>
            ///  Retrieves the window menu as a result of a keystroke. For more information, see the Remarks section.
            /// </summary>
            KEYMENU = 0xF100,

            /// <summary>
            ///  Maximizes the window.
            /// </summary>
            MAXIMIZE = 0xF030,

            /// <summary>
            ///  Minimizes the window.
            /// </summary>
            MINIMIZE = 0xF020,

            /// <summary>
            ///  Sets the state of the display. This command supports devices that have power-saving features, such as a battery-powered personal computer.
            /// </summary>
            MONITORPOWER = 0xF170,

            /// <summary>
            ///  Retrieves the window menu as a result of a mouse click.
            /// </summary>
            MOUSEMENU = 0xF090,

            /// <summary>
            ///  Moves the window.
            /// </summary>
            MOVE = 0xF010,

            /// <summary>
            ///  Moves to the next window.
            /// </summary>
            NEXTWINDOW = 0xF040,

            /// <summary>
            ///  Moves to the previous window.
            /// </summary>
            PREVWINDOW = 0xF050,

            /// <summary>
            ///  Restores the window to its normal position and size.
            /// </summary>
            RESTORE = 0xF120,

            /// <summary>
            ///  Executes the screen saver application specified in the [boot] section of the System.ini file.
            /// </summary>
            SCREENSAVE = 0xF140,

            /// <summary>
            ///  Sizes the window.
            /// </summary>
            SIZE = 0xF000,

            /// <summary>
            ///  Activates the Start menu.
            /// </summary>
            TASKLIST = 0xF130,

            /// <summary>
            ///  Scrolls vertically.
            /// </summary>
            VSCROLL = 0xF070,
        }
    }
}