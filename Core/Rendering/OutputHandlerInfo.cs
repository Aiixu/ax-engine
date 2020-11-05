using static Ax.Engine.Core.Native;

namespace Ax.Engine.Core.Rendering
{
    public struct OutputHandlerInfo
    {
        public CONSOLE_FONT_INFOEX font;
        public int frameDelay;

        public void SetFont(CONSOLE_FONT_INFOEX font) => this.font = font;
    }
}
