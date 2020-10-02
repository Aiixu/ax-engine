using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Writes a character string to a console screen buffer beginning at the current cursor location.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_WRITE"/> access right.</param>
        /// <param name="lpBuffer">A pointer to a buffer that contains characters to be written to the console screen buffer.</param>
        /// <param name="lpNumberOfCharsToWrite">The number of characters to be written. If the total size of the specified number of characters exceeds the available heap, the function fails with ERROR_NOT_ENOUGH_MEMORY.</param>
        /// <param name="lpNumberOfCharsToWritten">A pointer to a variable that receives the number of characters actually written.</param>
        /// <param name="lpReserved">Reserved; must be NULL.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool WriteConsole([In] IntPtr hConsoleOutput, [In] [MarshalAs(UnmanagedType.LPArray)] byte[] lpBuffer, [In] int lpNumberOfCharsToWrite, [Out, Optional] out int lpNumberOfCharsToWritten, IntPtr lpReserved);

        /// <summary>
        ///  Writes data directly to the console input buffer.
        /// </summary>
        /// <param name="hConsoleInput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_WRITE"/> access right.</param>
        /// <param name="lpBuffer">A pointer to an array of <see cref="INPUT_RECORD"/> structures that contain data to be written to the input buffer.</param>
        /// <param name="nLength">The number of input records to be written.</param>
        /// <param name="lpNumberOfEventsWritten">A pointer to a variable that receives the number of input records actually written.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool WriteConsoleInput([In] IntPtr hConsoleInput, [In] INPUT_RECORD[] lpBuffer, [In] uint nLength, [Out] out uint lpNumberOfEventsWritten);

        /// <summary>
        ///  Writes character and color attribute data to a specified rectangular block of character cells in a console screen buffer. The data to be written is taken from a correspondingly sized rectangular block at a specified location in the source buffer.
        /// </summary>
        /// <param name="hConsoleInput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_WRITE"/> access right.</param>
        /// <param name="lpBuffer">The data to be written to the console screen buffer. This pointer is treated as the origin of a two-dimensional array of <see cref="CHAR_INFO"/> structures whose size is specified by the <paramref name="dwBufferSize"/> parameter.</param>
        /// <param name="dwBufferSize">The size of the buffer pointed to by the <paramref name="lpBuffer"/> parameter, in character cells. The X member of the COORD structure is the number of columns; the Y member is the number of rows.</param>
        /// <param name="dwBufferCoord">The coordinates of the upper-left cell in the buffer pointed to by the lpBuffer parameter. The X member of the COORD structure is the column, and the Y member is the row.</param>
        /// <param name="lpWriteRegion">A pointer to a <see cref="SMALL_RECT"/> structure. On input, the structure members specify the upper-left and lower-right coordinates of the console screen buffer rectangle to write to. On output, the structure members specify the actual rectangle that was used.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool WriteConsoleOutput([In] IntPtr hConsoleOutput, [In] CHAR_INFO[] lpBuffer, [In] COORD dwBufferSize, [In] COORD dwBufferCoord, [In, Out] ref SMALL_RECT lpWriteRegion);

        /// <summary>
        ///  Copies a number of character attributes to consecutive cells of a console screen buffer, beginning at a specified location.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_WRITE"/> access right.</param>
        /// <param name="lpAttribute">The attributes to be used when writing to the console screen buffer.</param>
        /// <param name="nLength">The number of screen buffer character cells to which the attributes will be copied.</param>
        /// <param name="dwWriteCoord">A <see cref="COORD"/> structure that specifies the character coordinates of the first cell in the console screen buffer to which the attributes will be written.</param>
        /// <param name="lpNumberOfAttrsWritten">A pointer to a variable that receives the number of attributes actually written to the console screen buffer.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool WriteConsoleOutputAttribute([In] IntPtr hConsoleOutput, [In] ushort[] lpAttribute, [In] uint nLength, [In] COORD dwWriteCoord, [Out] out uint lpNumberOfAttrsWritten);

        /// <summary>
        ///  Copies a number of characters to consecutive cells of a console screen buffer, beginning at a specified location.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the <see cref="BUFFER_ACCESS_MODE.GENERIC_WRITE"/> access right.</param>
        /// <param name="lpCharacter">The characters to be written to the console screen buffer.</param>
        /// <param name="nLength">The number of characters to be written.</param>
        /// <param name="dwWriteCoord">A <see cref="COORD"/> structure that specifies the character coordinates of the first cell in the console screen buffer to which characters will be written.</param>
        /// <param name="lpNumberOfCharsWritten">A pointer to a variable that receives the number of characters actually written.</param>
        /// <returns>If the function succeeds, returns TRUE, otherwise, retun FALSE.</returns>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, string lpCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);
    }
}
