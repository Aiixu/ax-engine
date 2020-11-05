namespace Ax.Engine.Core.Native
{
    public static partial class WinUser
    {
        /// <summary>
        ///  The type of system command requested.
        /// </summary>
        public enum SCF : uint
        {
            /// <summary>
            ///  Indicates whether the screen saver is secure.
            /// </summary>
            ISSECURE = 0x00000001
        }
    }
}