namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public enum CONSOLE_MODE_OUTPUT : uint
        {
            ENABLE_PROCESSEDOUTPUT = 0x1,
            ENABLE_WRAPATEOLOUTPUT = 0x2,
            ENABLE_VIRTUALTERMINALPROCESSING = 0x4,
            DISABLE_NEWLINEAUTORETURN = 0x8,
            ENABLE_LVBGRIDWORLDWIDE = 0x10
        }
    }
}