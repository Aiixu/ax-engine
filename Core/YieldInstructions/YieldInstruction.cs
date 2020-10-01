using System.Collections;

namespace Ax.Engine.Core
{
    public abstract class YieldInstruction
    {
        internal IEnumerator routine;

        public YieldInstruction() 
        {
            routine = Routine();
        }

        public bool MoveNext()
        {
            return routine.Current is YieldInstruction yieldInstruction ?
                yieldInstruction.MoveNext() || routine.MoveNext() :
                routine.MoveNext();
        }

        internal abstract IEnumerator Routine();
    }
}
