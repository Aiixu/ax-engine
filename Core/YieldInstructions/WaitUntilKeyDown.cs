using System.Collections;

using static Ax.Engine.Core.Native.WinUser;

namespace Ax.Engine.Core
{
    public sealed class WaitUntilKeyDown : YieldInstruction
    {
        private readonly KEY key;

        public WaitUntilKeyDown(KEY key)
        {
            this.key = key;
        }

        internal override IEnumerator Routine()
        {
            yield return new WaitUntil(() => GameInput.GetKeyDown(key));
        }
    }
}
