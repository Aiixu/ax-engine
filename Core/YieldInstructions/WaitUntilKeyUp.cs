using System.Collections;

using static Ax.Engine.Core.Native.WinUser;

namespace Ax.Engine.Core
{
    public sealed class WaitUntilKeyUp : YieldInstruction
    {
        private readonly KEY key;

        public WaitUntilKeyUp(KEY key)
        {
            this.key = key;
        }

        internal override IEnumerator Routine()
        {
            yield return new WaitUntil(() => GameInput.GetKeyUp(key));
        }
    }
}
