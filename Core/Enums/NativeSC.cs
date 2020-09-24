﻿namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public enum SC : uint
        {
            CLOSE = 0xF060,
            CONTEXTHELP = 0xF180,
            DEFAULT = 0xF160,
            HOTKEY = 0xF150,
            HSSCROLL = 0xF080,
            KEYMENU = 0xF100,
            MAXIMIZE = 0xF030,
            MINIMIZE = 0xF020,
            MONITORPOWER = 0xF170,
            MOUSEMENU = 0xF090,
            MOVE = 0xF010,
            NEXTWINDOW = 0xF040,
            PREVWINDOW = 0xF050,
            RESTORE = 0xF120,
            SCREENSAVE = 0xF140,
            SIZE = 0xF000,
            TASKLIST = 0xF130,
            VSCROLL = 0xF070,
        }
    }
}