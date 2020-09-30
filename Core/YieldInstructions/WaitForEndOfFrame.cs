using System.Collections;

namespace Ax.Engine.Core
{
    public sealed class WaitForEndOfFrame : YieldInstruction
    {
        public WaitForEndOfFrame()
        {
            routine = Routine();
        }

        private IEnumerator Routine()
        {
            yield return true;
        }
    }
}
