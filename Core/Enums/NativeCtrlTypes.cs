namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public enum CTRL_TYPES : uint
        {
            C_EVENT = 0,
            BREAK_EVENT,
            CLOSE_EVENT,
            LOGOFF_EVENT = 5,
            SHUTDOWN_EVENT
        }
    }
}