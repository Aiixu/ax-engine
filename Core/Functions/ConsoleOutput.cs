using System;
using System.Runtime.InteropServices;

namespace Ax.Engine.Core
{
    public static partial class Native
    {
        /// <summary>
        ///  Sets the character attributes for a specified number of character cells, beginning at the specified coordinates in a screen buffer.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_WRITE access right.</param>
        /// <param name="wAttribute">The attributes to use when writing to the console screen buffer. See <see cref="CHAR_ATTRIBUTE"/></param>
        /// <param name="nLength">The number of character cells to be set to the specified color attributes.</param>
        /// <param name="dwWriteCoord">A <see cref="COORD"/> structure that specifies the character coordinates of the first cell whose attributes are to be set.</param>
        /// <param name="lpNumberOfAttrsWritten">A pointer to a variable that receives the number of character cells whose attributes were actually set.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool FillConsoleOutputAttribute(IntPtr hConsoleOutput, ushort wAttribute, uint nLength, COORD dwWriteCoord, out uint lpNumberOfAttrsWritten);

        /// <summary>
        ///  Writes a character to the console screen buffer a specified number of times, beginning at the specified coordinates.
        /// </summary>
        /// <param name="hConsoleOutput">A handle to the console screen buffer. The handle must have the GENERIC_WRITE access right.</param>
        /// <param name="cCharacter">The character to be written to the console screen buffer.</param>
        /// <param name="nLength">The number of character cells to which the character should be written.</param>
        /// <param name="dwWriteCoord">A <see cref="COORD"/> structure that specifies the character coordinates of the first cell to which the character is to be written.</param>
        /// <param name="lpNumberOfCharsWritten">A pointer to a variable that receives the number of characters actually written to the console screen buffer.</param>
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool FillConsoleOutputCharacter(IntPtr hConsoleOutput, char cCharacter, uint nLength, COORD dwWriteCoord, out uint lpNumberOfCharsWritten);
    }
}
