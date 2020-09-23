namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public enum CONSOLE_EVENTS
        {
            MOUSE_DOWN = 0x0008,
            MOUSE_SELECTION = 0x0004,
            NO_SELECTION = 0x0000,
            SELECTION_IN_PROGRESS = 0x0001,
            SELECTION_NOT_EMPTY = 0x0002
        }
    }
}