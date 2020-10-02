using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// TODO : SECURITY_ATTRIBUTES

        /// <summary>
        ///  Creates a console screen buffer.
        /// </summary>
        /// <param name="dwDesiredAccess">The access to the console screen buffer. See <see cref="BUFFER_ACCESS_MODE"/>.</param>
        /// <param name="dwShareMode">The sharing mode of the console screen buffer. See <see cref="BUFFER_SHARE_MODE"/></param>
        /// <param name="lpSecurityAttributes">A pointer to a <see cref="SECURITY_ATTRIBUTES"/> structure that determines whether the returned handle can be inherited by child processes. If <paramref name="lpSecurityAttributes"/> is <see langword="null"/>, the handle cannot be inherited. The <i>lpSecurityDescriptor</i> member of the structure specifies a security descriptor for the new console screen buffer. If <see cref="SECURITY_ATTRIBUTES.lpSecurityAttributes"/> is <paramref name="lpSecurityAttributes"/>, the console screen buffer gets a default security descriptor. The ACLs in the default security descriptor for a console screen buffer come from the primary or impersonation token of the creator.</param>
        /// <param name="dwFlags">The type of console screen buffer to create.</param>
        /// <param name="lpScreenBufferData">Reserved; should be <see langword="null"/>.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern IntPtr CreateConsoleScreenBuffer(long dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwFlags, IntPtr lpScreenBufferData);
    }
}
