using System.Collections;

namespace Ax.Engine.Core
{
    public sealed class WaitForFrameCount : YieldInstruction
    {
        private readonly int end;

        public WaitForFrameCount(int count)
        {
            end = Game.Instance.FrameCount + count;
        }

        internal override IEnumerator Routine()
        {
            yield return new WaitWhile(() => Game.Instance.FrameCount < end);
        }
    }
}
