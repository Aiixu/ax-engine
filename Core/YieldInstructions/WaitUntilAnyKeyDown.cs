using System.Collections;

namespace Ax.Engine.Core
{
    public sealed class WaitUntilAnyKeyDown : YieldInstruction
    {
        internal override IEnumerator Routine()
        {
            yield return new WaitUntil(() => InputHandler.GetKeyDown(KEY.Any));
        }
    }
}
