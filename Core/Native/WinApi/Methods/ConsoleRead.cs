using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Ax.Engine.Core.Native
{
    public static partial class WinApi
    {
        /// <summary>
        ///  Reads character input from the console input buffer and removes it from the buffer.
        /// </summary>
        /// <param name="hConsoleInput">A handle to the console input buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpBuffer">A pointer to a buffer that receives the data read from the console input buffer.</param>
        /// <param name="nNumberOfCharsToRead">The number of characters to be read.</param>
        /// <param name="lpNumberOfCharsRead">A pointer to a variable that receives the number of characters actually read.</param>
        /// <param name="pInputControl">
        ///  A <see cref="CONSOLE_READCONSOLE_CONTROL"/> structure that contains a control character to signal the end of the read operation. This parameter can be <see langword="null"/>.
        ///  This parameter requires Unicode input by default. For ANSI mode, set this parameter to see langword="null"/>.
        /// </param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool ReadConsole([In] IntPtr hConsoleInput, [Out] StringBuilder lpBuffer, [In] uint nNumberOfCharsToRead, [Out] out uint lpNumberOfCharsRead, [In, Optional] CONSOLE_READCONSOLE_CONTROL pInputControl);

        /// <summary>
        ///  Reads data from the specified console input buffer without removing it from the buffer.
        /// </summary>
        /// <param name="hConsoleInput">A handle to the console input buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpBuffer">A pointer to an array of <see cref="INPUT_RECORD"/> structures that receives the input buffer data.</param>
        /// <param name="nLength">The size of the array pointed to by the <paramref name="lpBuffer"/> parameter, in array elements.</param>
        /// <param name="lpNumberOfEventsRead">A pointer to a variable that receives the number of input records read.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool PeekConsoleInput([In] IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, [In] uint nLength, [Out] out uint lpNumberOfEventsRead);

        /// <summary>
        ///  Reads data from a console input buffer and removes it from the buffer.
        /// </summary>
        /// <param name="hConsoleInput">A handle to the console input buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpBuffer">A pointer to an array of <see cref="INPUT_RECORD"/> structures that receives the input buffer data.</param>
        /// <param name="nLength">he size of the array pointed to by the <paramref name="lpBuffer"/> parameter, in array elements.</param>
        /// <param name="lpNumberOfEventsRead">A pointer to a variable that receives the number of input records read.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)] public static extern bool ReadConsoleInput([In] IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, [In] uint nLength, [Out] out uint lpNumberOfEventsRead);

        /// <summary>
        ///  Reads character and color attribute data from a rectangular block of character cells in a console screen buffer, and the function writes the data to a rectangular block at a specified location in the destination buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpBuffer">A pointer to a destination buffer that receives the data read from the console screen buffer. This pointer is treated as the origin of a two-dimensional array of <see cref="CHAR_INFO"/> structures whose size is specified by the <paramref name="dwBufferSize"/> parameter.</param>
        /// <param name="dwBufferSize">The size of the lpBuffer parameter, in character cells. The X member of the <see cref="COORD"/> structure is the number of columns; the Y member is the number of rows.</param>
        /// <param name="dwBufferCoord">The coordinates of the upper-left cell in the <paramref name="lpBuffer"/> parameter that receives the data read from the console screen buffer. The X member of the <see cref="COORD"/> structure is the column, and the Y member is the row.</param>
        /// <param name="lpReadRegion">A pointer to a <see cref="SMALL_RECT"/> structure. On input, the structure members specify the upper-left and lower-right coordinates of the console screen buffer rectangle from which the function is to read. On output, the structure members specify the actual rectangle that was used.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool ReadConsoleOutput([In] IntPtr hConsoleOutput, [Out] CHAR_INFO[] lpBuffer, [In] COORD dwBufferSize, [In] COORD dwBufferCoord, [In, Out] SMALL_RECT lpReadRegion);

        /// <summary>
        ///  Copies a number of characters from consecutive cells of a console screen buffer, beginning at a specified location.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpCharacter">A pointer to a buffer that receives the characters read from the console screen buffer.</param>
        /// <param name="nLength">The number of screen buffer character cells from which to read.</param>
        /// <param name="dwReadCoord">The coordinates of the first cell in the console screen buffer from which to read, in characters. The X member of the <see cref="COORD"/> structure is the column, and the Y member is the row.</param>
        /// <param name="lpNumberOfCharsRead">A pointer to a variable that receives the number of characters actually read.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool ReadConsoleOutputCharacter([In] IntPtr hConsoleOutput, [Out] StringBuilder lpCharacter, [In] uint nLength, [In] COORD dwReadCoord, [Out] out uint lpNumberOfCharsRead);

        /// <summary>
        ///  Copies a number of characters from consecutive cells of a console screen buffer, beginning at a specified location.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_READ"/> access right.</param>
        /// <param name="lpAttribute">A pointer to a buffer that receives the attributes being used by the console screen buffer.</param>
        /// <param name="nLength">The number of screen buffer character cells from which to read.</param>
        /// <param name="dwReadCoord">The coordinates of the first cell in the console screen buffer from which to read, in characters. The X member of the <see cref="COORD"/> structure is the column, and the Y member is the row.</param>
        /// <param name="lpNumberOfAttrsRead">A pointer to a variable that receives the number of attributes actually read.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool ReadConsoleOutputAttribute([In] IntPtr hConsoleOutput, [Out] ushort[] lpAttribute, [In] uint nLength, [In] COORD dwReadCoord, [Out] out uint lpNumberOfAttrsRead);
    }
}
