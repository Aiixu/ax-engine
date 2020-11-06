namespace Ax.Engine.Core.Native
{
    public static partial class WinUser
    {
        /// <summary>
        ///  System metrics flags.
        /// </summary>
        public enum SM : int
        {
            /// <summary>
            ///  Specify how the system arranged minimized windows. For more information, see the Remarks section in this topic.
            /// </summary>
            ARRANGE = 0x56,

            /// <summary>
            /// The value that specifies how the system is started:
            ///  <list type="bullet">
            ///     <item>0 Normal boot</item>   
            ///     <item>1 Fail-safe boot</item>   
            ///     <item>2 Fail-safe with network boot</item>   
            ///  </list> 
            ///  A fail-safe boot (also called SafeBoot, Safe Mode, or Clean Boot) bypasses the user startup files.
            /// </summary>
            CLEANBOOT = 0x67,

            /// <summary>
            ///  The number of display monitors on a desktop. For more information, see the Remarks section in this topic.
            /// </summary>
            CMONITORS = 0x80,

            /// <summary>
            ///  The number of buttons on a mouse, or zero if no mouse is installed.
            /// </summary>
            CMOUSEBUTTONS = 0x43,

            /// <summary>
            ///  Reflects the state of the laptop or slate mode, 0 for Slate Mode and non-zero otherwise.
            /// </summary>
            CONVERTIBLESLATEMODE = 0x2003,

            /// <summary>
            ///  The width of a window border, in pixels. This is equivalent to the SM_CXEDGE value for windows with the 3-D look.
            /// </summary>
            CXBORDER = 0x5,

            /// <summary>
            ///  The width of a cursor, in pixels. The system cannot create cursors of other sizes.
            /// </summary>
            CXCURSOR = 0x13,

            /// <summary>
            ///  This value is the same as <see cref="CXFIXEDFRAME"/>.
            /// </summary>
            CXDLGFRAME = 0x7,

            /// <summary>
            ///  The width of the rectangle around the location of a first click in a double-click sequence, in pixels.
            /// </summary>
            CXDOUBLECLK = 0x36,

            /// <summary>
            ///  The number of pixels on either side of a mouse-down point that the mouse pointer can move before a drag operation begins.
            /// </summary>
            CXDRAG = 0x68,

            /// <summary>
            ///  The width of a 3-D border, in pixels. This metric is the 3-D counterpart of <see cref="CXBORDER"/>.
            /// </summary>
            CXEDGE = 0x45,

            /// <summary>
            ///  The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
            /// </summary>
            CXFIXEDFRAME = 0x7,

            /// <summary>
            ///  The width of the left and right edges of the focus rectangle that the DrawFocusRect draws, in pixels.
            /// </summary>
            CXFOCUSBORDER = 0x83,

            /// <summary>
            ///  This value is the same as SM_CXSIZEFRAME.
            /// </summary>
            CXFRAME = 0x32,

            /// <summary>
            ///  The width of the client area for a full-screen window on the primary display monitor, in pixels.
            /// </summary>
            CXFULLSCREEN = 0x16,

            /// <summary>
            ///  The width of the arrow bitmap on a horizontal scroll bar, in pixels.
            /// </summary>
            CXHSCROLL = 0x21,

            /// <summary>
            ///  The width of the thumb box in a horizontal scroll bar, in pixels.
            /// </summary>
            CXHTHUMB = 0x10,

            /// <summary>
            ///  The default width of an icon, in pixels.
            /// </summary>
            CXICON = 0x11,

            /// <summary>
            ///  The width of a grid cell for items in large icon view, in pixels.
            /// </summary>
            CXICONSPACING = 0x38,

            /// <summary>
            ///  The default width, in pixels, of a maximized top-level window on the primary display monitor.
            /// </summary>
            CXMAXIMIZED = 0x61,

            /// <summary>
            ///  The default maximum width of a window that has a caption and sizing borders, in pixels. 
            /// </summary>
            CXMAXTRACK = 0x59,

            /// <summary>
            ///  The width of the default menu check-mark bitmap, in pixels.
            /// </summary>
            CXMENUCHECK = 0x71,

            /// <summary>
            ///  The width of menu bar buttons, such as the child window close button that is used in the multiple document interface, in pixels.
            /// </summary>
            CXMENUSIZE = 0x54,

            /// <summary>
            ///  The minimum width of a window, in pixels.
            /// </summary>
            CXMIN = 0x28,

            /// <summary>
            ///  The width of a minimized window, in pixels.
            /// </summary>
            CXMINIMIZED = 0x57,

            /// <summary>
            ///  The width of a grid cell for a minimized window, in pixels.
            /// </summary>
            CXMINSPACING = 0x47,

            /// <summary>
            ///  The minimum tracking width of a window, in pixels. 
            /// </summary>
            CXMINTRACK = 0x34,

            /// <summary>
            ///  The amount of border padding for captioned windows, in pixels.
            /// </summary>
            CXPADDEDBORDER = 0x92,

            /// <summary>
            ///  The width of the screen of the primary display monitor, in pixels.
            /// </summary>
            CXSCREEN = 0x0,

            /// <summary>
            ///  The width of a button in a window caption or title bar, in pixels.
            /// </summary>
            CXSIZE = 0x30,

            /// <summary>
            ///  The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
            /// </summary>
            CXSIZEFRAME = 0x32,

            /// <summary>
            ///  The recommended width of a small icon, in pixels.
            /// </summary>
            CXSMICON = 0x49,

            /// <summary>
            ///  The width of small caption buttons, in pixels.
            /// </summary>
            CXSMSIZE = 0x52,

            /// <summary>
            ///  The width of the virtual screen, in pixels.
            /// </summary>
            CXVIRTUALSCREEN = 0x78,

            /// <summary>
            ///  The width of a vertical scroll bar, in pixels.
            /// </summary>
            CXVSCROLL = 0x2,

            /// <summary>
            ///  The height of a window border, in pixels. 
            /// </summary>
            CYBORDER = 0x6,

            /// <summary>
            ///  The height of a caption area, in pixels.
            /// </summary>
            CYCAPTION = 0x4,

            /// <summary>
            ///  The height of a cursor, in pixels. The system cannot create cursors of other sizes.
            /// </summary>
            CYCURSOR = 0x14,

            /// <summary>
            ///  This value is the same as <see cref="CYFIXEDFRAME"/>.
            /// </summary>
            CYDLGFRAME = 0x8,

            /// <summary>
            ///  The height of the rectangle around the location of a first click in a double-click sequence, in pixels.
            /// </summary>
            CYDOUBLECLK = 0x37,

            /// <summary>
            ///  The number of pixels above and below a mouse-down point that the mouse pointer can move before a drag operation begins.
            /// </summary>
            CYDRAG = 0x69,

            /// <summary>
            ///  The height of a 3-D border, in pixels.
            /// </summary>
            CYEDGE = 0x46,

            /// <summary>
            ///  The thickness of the frame around the perimeter of a window that has a caption but is not sizable, in pixels.
            /// </summary>
            CYFIXEDFRAME = 0x8,

            /// <summary>
            ///  The height of the top and bottom edges of the focus rectangle drawn by DrawFocusRect, in pixels.
            /// </summary>
            CYFOCUSBORDER = 0x84,

            /// <summary>
            ///  This value is the same as SM_CYSIZEFRAME.
            /// </summary>
            CYFRAME = 0x33,

            /// <summary>
            ///  The height of the client area for a full-screen window on the primary display monitor, in pixels. 
            /// </summary>
            CYFULLSCREEN = 0x17,

            /// <summary>
            ///  The height of a horizontal scroll bar, in pixels.
            /// </summary>
            CYHSCROLL = 0x3,

            /// <summary>
            ///  The default height of an icon, in pixels.
            /// </summary>
            CYICON = 0x12,

            /// <summary>
            ///  The height of a grid cell for items in large icon view, in pixels.
            /// </summary>
            CYICONSPACING = 0x39,

            /// <summary>
            ///  For double byte character set versions of the system, this is the height of the Kanji window at the bottom of the screen, in pixels.
            /// </summary>
            CYKANJIWINDOW = 0x18,

            /// <summary>
            ///  The default height, in pixels, of a maximized top-level window on the primary display monitor.
            /// </summary>
            CYMAXIMIZED = 0x62,

            /// <summary>
            ///  The default maximum height of a window that has a caption and sizing borders, in pixels.
            /// </summary>
            CYMAXTRACK = 0x60,

            /// <summary>
            ///  The height of a single-line menu bar, in pixels.
            /// </summary>
            CYMENU = 0x15,

            /// <summary>
            ///  The height of the default menu check-mark bitmap, in pixels.
            /// </summary>
            CYMENUCHECK = 0x72,

            /// <summary>
            ///  The height of menu bar buttons, such as the child window close button that is used in the multiple document interface, in pixels.
            /// </summary>
            CYMENUSIZE = 0x55,

            /// <summary>
            ///  The minimum height of a window, in pixels.
            /// </summary>
            CYMIN = 0x29,

            /// <summary>
            ///  The height of a minimized window, in pixels.
            /// </summary>
            CYMINIMIZED = 0x58,

            /// <summary>
            ///  The height of a grid cell for a minimized window, in pixels.
            /// </summary>
            CYMINSPACING = 0x48,

            /// <summary>
            ///  The minimum tracking height of a window, in pixels.
            /// </summary>
            CYMINTRACK = 0x35,

            /// <summary>
            ///  The height of the screen of the primary display monitor, in pixels.
            /// </summary>
            CYSCREEN = 0x1,

            /// <summary>
            ///  The height of a button in a window caption or title bar, in pixels.
            /// </summary>
            CYSIZE = 0x31,

            /// <summary>
            ///  The thickness of the sizing border around the perimeter of a window that can be resized, in pixels.
            /// </summary>
            CYSIZEFRAME = 0x33,

            /// <summary>
            ///  The height of a small caption, in pixels.
            /// </summary>
            CYSMCAPTION = 0x51,

            /// <summary>
            ///  The recommended height of a small icon, in pixels. 
            /// </summary>
            CYSMICON = 0x50,

            /// <summary>
            ///  The height of small caption buttons, in pixels.
            /// </summary>
            CYSMSIZE = 0x53,

            /// <summary>
            ///  The height of the virtual screen, in pixels.
            /// </summary>
            CYVIRTUALSCREEN = 0x79,

            /// <summary>
            ///  The height of the arrow bitmap on a vertical scroll bar, in pixels.
            /// </summary>
            CYVSCROLL = 0x20,

            /// <summary>
            ///  The height of the thumb box in a vertical scroll bar, in pixels.
            /// </summary>
            CYVTHUMB = 0x9,

            /// <summary>
            ///  Nonzero if User32.dll supports DBCS; otherwise, 0.
            /// </summary>
            DBCSENABLED = 0x42,

            /// <summary>
            ///  Nonzero if the debug version of User.exe is installed; otherwise, 0.
            /// </summary>
            SM_DEBUG = 0x22,

            /// <summary>
            ///  Nonzero if the current operating system is Windows 7 or Windows Server 2008 R2 and the Tablet PC Input service is started; otherwise, 0.
            /// </summary>
            SM_DIGITIZER = 0x94,

            /// <summary>
            ///  Nonzero if Input Method Manager/Input Method Editor features are enabled; otherwise, 0.
            /// </summary>
            IMMENABLED = 0x82,

            /// <summary>
            ///  Nonzero if there are digitizers in the system; otherwise, 0.
            /// </summary>
            MAXIMUMTOUCHES = 0x95,

            /// <summary>
            ///  Nonzero if the current operating system is the Windows XP, Media Center Edition, 0 if not.
            /// </summary>
            MEDIACENTER = 0x87,

            /// <summary>
            ///  Nonzero if drop-down menus are right-aligned with the corresponding menu-bar item; 0 if the menus are left-aligned.
            /// </summary>
            MENUDROPALIGNMENT = 0x40,

            /// <summary>
            ///  Nonzero if the system is enabled for Hebrew and Arabic languages, 0 if not.
            /// </summary>
            MIDEASTENABLED = 0x74,

            /// <summary>
            ///  Nonzero if a mouse is installed; otherwise, 0.
            /// </summary>
            MOUSEPRESENT = 0x19,

            /// <summary>
            ///  Nonzero if a mouse with a horizontal scroll wheel is installed; otherwise 0.
            /// </summary>
            MOUSEHORIZONTALWHEELPRESENT = 0x91,

            /// <summary>
            ///  Nonzero if a mouse with a vertical scroll wheel is installed; otherwise 0.
            /// </summary>
            MOUSEWHEELPRESENT = 0x75,

            /// <summary>
            ///  The least significant bit is set if a network is present; otherwise, it is cleared. The other bits are reserved for future use.
            /// </summary>
            NETWORK = 0x63,

            /// <summary>
            ///  Nonzero if the Microsoft Windows for Pen computing extensions are installed; zero otherwise.
            /// </summary>
            PENWINDOWS = 0x41,

            /// <summary>
            ///  This system metric is used in a Terminal Services environment to determine if the current Terminal Server session is being remotely controlled. Its value is nonzero if the current session is remotely controlled; otherwise, 0.
            /// </summary>
            REMOTECONTROL = 0x2001,

            /// <summary>
            ///  This system metric is used in a Terminal Services environment. If the calling process is associated with a Terminal Services client session, the return value is nonzero. If the calling process is associated with the Terminal Services console session, the return value is 0.
            /// </summary>
            REMOTESESSION = 0x1000,

            /// <summary>
            ///  Nonzero if all the display monitors have the same color format, otherwise, 0. Two displays can have the same bit depth, but different color formats. For example, the red, green, and blue pixels can be encoded with different numbers of bits, or those bits can be located in different places in a pixel color value.
            /// </summary>
            SAMEDISPLAYFORMAT = 0x81,

            /// <summary>
            ///  This system metric should be ignored; it always returns 0.
            /// </summary>
            SECURE = 0x44,

            /// <summary>
            ///  The build number if the system is Windows Server 2003 R2; otherwise, 0.
            /// </summary>
            SERVERR2 = 0x89,

            /// <summary>
            ///  Nonzero if the user requires an application to present information visually in situations where it would otherwise present the information only in audible form; otherwise, 0.
            /// </summary>
            SHOWSOUNDS = 0x70,

            /// <summary>
            ///  Nonzero if the current session is shutting down; otherwise, 0.
            /// </summary>
            SHUTTINGDOWN = 0x2000,

            /// <summary>
            ///  Nonzero if the computer has a low-end (slow) processor; otherwise, 0.
            /// </summary>
            SLOWMACHINE = 0x73,

            /// <summary>
            ///  Nonzero if the current operating system is Windows 7 Starter Edition, Windows Vista Starter, or Windows XP Starter Edition; otherwise, 0.
            /// </summary>
            STARTER = 0x88,

            /// <summary>
            ///  Nonzero if the meanings of the left and right mouse buttons are swapped; otherwise, 0.
            /// </summary>
            SWAPBUTTON = 0x23,

            /// <summary>
            ///  Reflects the state of the docking mode, 0 for Undocked Mode and non-zero otherwise. 
            /// </summary>
            SYSTEMDOCKED = 0x2004,

            /// <summary>
            ///  Nonzero if the current operating system is the Windows XP Tablet PC edition or if the current operating system is Windows Vista or Windows 7 and the Tablet PC Input service is started; otherwise, 0.
            /// </summary>
            TABLETPC = 0x86,

            /// <summary>
            ///  The coordinates for the left side of the virtual screen. The virtual screen is the bounding rectangle of all display monitors.
            /// </summary>
            XVIRTUALSCREEN = 0x76,

            /// <summary>
            ///  The coordinates for the top of the virtual screen. The virtual screen is the bounding rectangle of all display monitors.
            /// </summary>
            YVIRTUALSCREEN = 0x77
        }
    }
}
