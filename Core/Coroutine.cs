using System.Collections;

namespace Ax.Engine.Core
{
	public sealed class Coroutine : YieldInstruction
	{
		internal Coroutine(IEnumerator routine)
		{
			this.routine = routine;
		}

        internal override IEnumerator Routine()
        {
			yield return null;
        }
    }
}
