using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinUser
    {
        /// <summary>
        ///  Retrieves the specified system metric or system configuration setting in pixels.
        /// </summary>
        /// <param name="nIndex">The system metric or configuration setting to be retrieved. See <see cref="SM"/></param>
        /// <returns>If the function succeeds, the return value is the requested system metric or configuration setting. If the function fails, the return value is 0.</returns>
        [DllImport("user32.dll")] public static extern int GetSystemMetrics([In] int nIndex);
    }
}
