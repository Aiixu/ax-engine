using System.Collections;

namespace Ax.Engine.Core
{
    public class YieldInstruction
    {
        public IEnumerator routine;

        public YieldInstruction() { }

        public bool MoveNext()
        {
            return routine.Current is YieldInstruction yieldInstruction ?
                yieldInstruction.MoveNext() || routine.MoveNext() :
                routine.MoveNext();
        }
    }
}
