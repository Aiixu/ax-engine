using System;
using System.Collections;

namespace Ax.Engine.Core
{
    public sealed class WaitForSeconds : YieldInstruction
    {
        private DateTime start;
        private int delay;

        public WaitForSeconds(int seconds)
        {
            start = DateTime.Now;
            delay = seconds;
            routine = Routine();
        }

        private IEnumerator Routine()
        {
            while ((DateTime.Now - start).TotalSeconds < delay)
            {
                yield return false;
            }
        }
    }
}
