namespace Ax.Engine.Core
{
    public static partial class Native
    {
        public enum MF : long
        {
            BITMAP = 0x00000004L,
            CHECKED = 0x00000008L,
            DISABLED = 0x00000002L,
            ENABLED = 0x00000000L,
            GRAYED = 0x00000001L,
            MENUBARBREAK = 0x00000020L,
            MENUBREAK = 0x00000040L,
            OWNERDRAW = 0x00000100L,
            POPUP = 0x00000010L,
            SEPARATOR = 0x00000800L,
            STRING = 0x00000000L,
            UNCHECKED = 0x00000000L,
            BYCOMMAND = 0x00000000
        }
    }
}