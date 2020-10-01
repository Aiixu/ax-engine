using System;
using System.Collections;

namespace Ax.Engine.Core
{
    public sealed class WaitUntil : YieldInstruction
    {
        private Func<bool> predicate;

        public WaitUntil(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        internal override IEnumerator Routine()
        {
            yield return !predicate.Invoke();
        }
    }
}
