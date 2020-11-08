using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// TODO : SECURITY_ATTRIBUTES, GENERIC_READ

        /// <summary>
        ///  Creates a console screen buffer.
        /// </summary>
        /// <param name="dwDesiredAccess">The access to the console screen buffer. See <see cref="BUFFER_ACCESS_MODE"/>.</param>
        /// <param name="dwShareMode">The sharing mode of the console screen buffer. See <see cref="BUFFER_SHARE_MODE"/></param>
        /// <param name="lpSecurityAttributes">A pointer to a <see cref="SECURITY_ATTRIBUTES"/> structure that determines whether the returned handle can be inherited by child processes. If <paramref name="lpSecurityAttributes"/> is <see langword="null"/>, the handle cannot be inherited. The <i>lpSecurityDescriptor</i> member of the structure specifies a security descriptor for the new console screen buffer. If <see cref="SECURITY_ATTRIBUTES.lpSecurityAttributes"/> is <paramref name="lpSecurityAttributes"/>, the console screen buffer gets a default security descriptor. The ACLs in the default security descriptor for a console screen buffer come from the primary or impersonation token of the creator.</param>
        /// <param name="dwFlags">The type of console screen buffer to create.</param>
        /// <param name="lpScreenBufferData">Reserved; should be <see langword="null"/>.</param>
        /// <returns>If the function succeeds, the return value is a handle to the new console screen buffer.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern IntPtr CreateConsoleScreenBuffer([In] uint dwDesiredAccess, [In] uint dwShareMode, [In, Optional] IntPtr lpSecurityAttributes, [In] uint dwFlags, IntPtr lpScreenBufferData);

        /// <summary>
        ///  Retrieves information about the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_READ access right</param>
        /// <param name="lpConsoleScreenBufferInfo">A <see cref="CONSOLE_sc"/> structure that contains the console screen buffer information.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleScreenBufferInfo([In] IntPtr hConsoleOutput, [Out] out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

        /// <summary>
        ///  Changes the size of the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_WRITE"/> access right.</param>
        /// <param name="dwSize">A <see cref=""/> structure that specifies the new size of the console screen buffer, in character rows and columns. The specified width and height cannot be less than the width and height of the console screen buffer's window. The specified dimensions also cannot be less than the minimum size allowed by the system.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleScreenBufferSize([In] IntPtr hConsoleOutput, [In] COORD dwSize);

        /// <summary>
        ///  
        /// </summary>
        /// <param name="hConsoleOutput"></param>
        /// <param name="ConsoleScreenBufferInfo"></param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool GetConsoleScreenBufferInfoEx([In] IntPtr hConsoleOutput, [In, Out] ref CONSOLE_SCREEN_BUFFER_INFOEX ConsoleScreenBufferInfo);

        /// <summary>
        ///  Sets extended information about the specified console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpConsoleScreenBufferInfoEx">A <see cref="CONSOLE_SCREEN_BUFFER_INFOEX"/> structure that contains the console screen buffer information.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleScreenBufferInfoEx([In] IntPtr hConsoleOutput, [In] CONSOLE_SCREEN_BUFFER_INFOEX lpConsoleScreenBufferInfoEx);

        /// <summary>
        ///  Sets the specified screen buffer to be the currently displayed console screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool SetConsoleActiveScreenBuffer([In] IntPtr hConsoleOutput);

        /// <summary>
        ///  Moves a block of data in a screen buffer. The effects of the move can be limited by specifying a clipping rectangle, so the contents of the console screen buffer outside the clipping rectangle are unchanged.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console input buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpScrollRectangle">A pointer to a <see cref="SMALL_RECT"/> structure whose members specify the upper-left and lower-right coordinates of the console screen buffer rectangle to be moved.</param>
        /// <param name="lpClipRectangle">A pointer to a <see cref="SMALL_RECT"/> structure whose members specify the upper-left and lower-right coordinates of the console screen buffer rectangle that is affected by the scrolling. This pointer can be <see langword="null"/>.</param>
        /// <param name="dwDestinationOrigin">A <see cref="COORD"/> structure that specifies the upper-left corner of the new location of the <paramref name="lpScrollRectangle"/> contents, in characters.</param>
        /// <param name="lpFill">A pointer to a <see cref="CHAR_INFO"/> structure that specifies the character and color attributes to be used in filling the cells within the intersection of <paramref name="lpScrollRectangle"/> and <paramref name="lpClipRectangle"/> that were left empty as a result of the move.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool ScrollConsoleScreenBuffer([In] IntPtr hConsoleOutput, [In] ref SMALL_RECT lpScrollRectangle, [In, Optional] IntPtr lpClipRectangle, [In] COORD dwDestinationOrigin, [In] ref CHAR_INFO lpFill);
    }
}
