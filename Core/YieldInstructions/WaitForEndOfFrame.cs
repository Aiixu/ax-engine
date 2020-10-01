using System.Collections;

namespace Ax.Engine.Core
{
    public sealed class WaitForEndOfFrame : YieldInstruction
    {
        public WaitForEndOfFrame() { }

        internal override IEnumerator Routine()
        {
            yield return true;
        }
    }
}
