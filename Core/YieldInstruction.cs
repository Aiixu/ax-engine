using System.Collections;

namespace Ax.Engine.Core
{
    public class YieldInstruction
    {
        public IEnumerator routine;
        
        public YieldInstruction() { }
        
        public bool MoveNext()
        {
            YieldInstruction yieldInstruction = (YieldInstruction)routine.Current;

            return yieldInstruction?.MoveNext() ?? routine.MoveNext();
        }
    }
}
