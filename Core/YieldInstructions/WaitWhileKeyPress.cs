using System.Collections;

using static Ax.Engine.Core.Native.WinUser;

namespace Ax.Engine.Core
{
    public sealed class WaitWhileKeyPress : YieldInstruction
    {
        private readonly KEY key;

        public WaitWhileKeyPress(KEY key)
        {
            this.key = key;
        }

        internal override IEnumerator Routine()
        {
            yield return new WaitWhile(() => GameInput.GetKey(key));
        }
    }
}
