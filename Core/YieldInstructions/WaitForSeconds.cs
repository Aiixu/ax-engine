namespace Ax.Engine.Core
{
    public sealed class WaitForSeconds : WaitForMilliseconds
    {
        public WaitForSeconds(int seconds) : base(seconds * 1000) 
        { }
    }
}
