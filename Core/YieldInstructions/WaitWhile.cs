using System;
using System.Collections;

namespace Ax.Engine.Core
{
    public sealed class WaitWhile : YieldInstruction
    {
        private readonly Func<bool> predicate;

        public WaitWhile(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        internal override IEnumerator Routine()
        {
            while (predicate.Invoke())
            {
                yield return false;
            }
        }
    }
}
