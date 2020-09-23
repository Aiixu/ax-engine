namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public enum CONSOLE_MODE_INPUT : uint
        {
            ENABLE_PROCESSEDINPUT = 0x1,
            ENABLE_LINEINPUT = 0x2,
            ENABLE_ECHOINPUT = 0x4,
            ENABLE_WINDOWINPUT = 0x8,
            ENABLE_MOUSEINPUT = 0x10,
            ENABLE_INSERTMODE = 0x20,
            ENABLE_QUICKEDITMODE = 0x40,
            ENABLE_EXTENDEDFLAGS = 0x80,
            ENABLE_AUTOPOSITION = 0x100,
            ENABLE_VIRTUALTERMINALINPUT = 0x200
        }
    }
}