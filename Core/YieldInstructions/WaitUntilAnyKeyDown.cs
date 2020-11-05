using System.Collections;

using static Ax.Engine.Core.Native.WinUser;

namespace Ax.Engine.Core
{
    public sealed class WaitUntilAnyKeyDown : YieldInstruction
    {
        internal override IEnumerator Routine()
        {
            yield return new WaitUntil(() => GameInput.GetKeyDown(KEY.Any));
        }
    }
}
