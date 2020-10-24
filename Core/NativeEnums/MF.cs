namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Controls the interpretation of the <i>uPosition</i> parameter and the content, appearance, and behavior of the new menu item.
        /// </summary>
        public enum MF : long
        {
            /// <summary>
            ///  Indicates that the uPosition parameter gives the identifier of the menu item. The <see cref="BYCOMMAND"/> flag is the default if neither the <see cref="BYCOMMAND"/> nor <see cref="BYPOSITION"/> flag is specified.
            /// </summary>
            BYCOMMAND = 0x00000000,

            /// <summary>
            ///  Indicates that the <i>uPosition</i> parameter gives the zero-based relative position of the new menu item. If <i>uPosition</i> is -1, the new menu item is appended to the end of the menu.
            /// </summary>
            BYPOSITION = 0x00000400L,

            /// <summary>
            ///  Uses a bitmap as the menu item. The lpNewItem parameter contains a handle to the bitmap.
            /// </summary>
            BITMAP = 0x00000004L,

            /// <summary>
            ///  Places a check mark next to the menu item. If the application provides check-mark bitmaps, this flag displays the check-mark bitmap next to the menu item.
            /// </summary>
            CHECKED = 0x00000008L,

            /// <summary>
            ///  Disables the menu item so that it cannot be selected, but does not gray it.
            /// </summary>
            DISABLED = 0x00000002L,

            /// <summary>
            ///  Enables the menu item so that it can be selected and restores it from its grayed state.
            /// </summary>
            ENABLED = 0x00000000L,

            /// <summary>
            ///  Disables the menu item and grays it so it cannot be selected.
            /// </summary>
            GRAYED = 0x00000001L,

            /// <summary>
            ///  Functions the same as the <see cref="MENUBREAK"/> flag for a menu bar. For a drop-down menu, submenu, or shortcut menu, the new column is separated from the old column by a vertical line.
            /// </summary>
            MENUBARBREAK = 0x00000020L,

            /// <summary>
            ///  Places the item on a new line (for menu bars) or in a new column (for a drop-down menu, submenu, or shortcut menu) without separating columns.
            /// </summary>
            MENUBREAK = 0x00000040L,

            /// <summary>
            ///  Specifies that the item is an owner-drawn item.
            /// </summary>
            OWNERDRAW = 0x00000100L,

            /// <summary>
            ///  Specifies that the menu item opens a drop-down menu or submenu. The uIDNewItem parameter specifies a handle to the drop-down menu or submenu. This flag is used to add a menu name to a menu bar or a menu item that opens a submenu to a drop-down menu, submenu, or shortcut menu.
            /// </summary>
            POPUP = 0x00000010L,

            /// <summary>
            ///  Draws a horizontal dividing line. This flag is used only in a drop-down menu, submenu, or shortcut menu. The line cannot be grayed, disabled, or highlighted. The <i>lpNewItem</i> and <i>uIDNewItem</i> parameters are ignored.
            /// </summary>
            SEPARATOR = 0x00000800L,

            /// <summary>
            ///  Specifies that the menu item is a text string; the lpNewItem parameter is a pointer to the string.
            /// </summary>
            STRING = 0x00000000L,

            /// <summary>
            ///  Does not place a check mark next to the menu item (default). If the application supplies check-mark bitmaps, this flag displays the clear bitmap next to the menu item.
            /// </summary>
            UNCHECKED = 0x00000000L,
        }
    }
}