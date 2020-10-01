using System;
using System.Collections;

namespace Ax.Engine.Core
{
    public class WaitForMilliseconds : YieldInstruction
    {
        private readonly DateTime start;
        private readonly int delay;

        public WaitForMilliseconds(int milliseconds)
        {
            delay = milliseconds;
            start = DateTime.Now;
        }

        internal override IEnumerator Routine()
        {
            yield return new WaitWhile(() => (DateTime.Now - start).TotalMilliseconds < delay);
        }
    }
}
