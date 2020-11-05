using System;

namespace Ax.Engine.Core.Rendering
{
    public struct OutputHandlerBufferInfo
    {
        public readonly IntPtr ptr;
        public readonly uint lastMode;

        public OutputHandlerBufferInfo(IntPtr ptr, uint lastMode)
        {
            this.ptr = ptr;
            this.lastMode = lastMode;
        }
    }
}
