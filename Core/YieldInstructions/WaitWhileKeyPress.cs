using System.Collections;

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
