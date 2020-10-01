using System;
using System.Collections;

namespace Ax.Engine.Core
{
    public sealed class WaitWhile : YieldInstruction
    {
        private Func<bool> predicate;

        public WaitWhile(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        internal override IEnumerator Routine()
        {
            yield return predicate.Invoke();
        }
    }
}
