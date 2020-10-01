using System.Collections;

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
            yield return new WaitUntil(() => InputHandler.GetKeyUp(key));
        }
    }
}
