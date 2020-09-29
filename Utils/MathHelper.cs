using System;

namespace Ax.Engine.Utils
{
    public static class MathHelper
    {
        #region Clamp Implementation
        public static decimal Clamp(decimal value, decimal min, decimal max) => Math.Min(Math.Max(value, min), max);

        public static float Clamp(float value, float min, float max) => Math.Min(Math.Max(value, min), max);
        public static double Clamp(double value, double min, double max) => Math.Min(Math.Max(value, min), max);

        public static byte Clamp(byte value, byte min, byte max) => Math.Min(Math.Max(value, min), max);
        public static sbyte Clamp(sbyte value, sbyte min, sbyte max) => Math.Min(Math.Max(value, min), max);

        public static short Clamp(short value, short min, short max) => Math.Min(Math.Max(value, min), max);
        public static ushort Clamp(ushort value, ushort min, ushort max) => Math.Min(Math.Max(value, min), max);

        public static long Clamp(long value, long min, long max) => Math.Min(Math.Max(value, min), max);
        public static ulong Clamp(ulong value, ulong min, ulong max) => Math.Min(Math.Max(value, min), max);

        public static int Clamp(int value, int min, int max) => Math.Min(Math.Max(value, min), max);
        public static uint Clamp(uint value, uint min, uint max) => Math.Min(Math.Max(value, min), max);
        #endregion

        #region ClampMax Implementation
        public static decimal ClampMax(decimal value, decimal max) => Math.Min(value, max);

        public static float ClampMax(float value, float max) => Math.Min(value, max);
        public static double ClampMax(double value, double max) => Math.Min(value, max);

        public static byte ClampMax(byte value, byte max) => Math.Min(value, max);
        public static sbyte ClampMax(sbyte value, sbyte max) => Math.Min(value, max);

        public static short ClampMax(short value, short max) => Math.Min(value, max);
        public static ushort ClampMax(ushort value, ushort max) => Math.Min(value, max);

        public static long ClampMax(long value, long max) => Math.Min(value, max);
        public static ulong ClampMax(ulong value, ulong max) => Math.Min(value, max);

        public static int ClampMax(int value, int max) => Math.Min(value, max);
        public static uint ClampMax(uint value, uint max) => Math.Min(value, max);
        #endregion

        #region ClampMin Implementation
        public static decimal ClampMin(decimal value, decimal min) => Math.Max(value, min);

        public static float ClampMin(float value, float min) => Math.Max(value, min);
        public static double ClampMin(double value, double min) => Math.Max(value, min);

        public static byte ClampMin(byte value, byte min) => Math.Max(value, min);
        public static sbyte ClampMin(sbyte value, sbyte min) => Math.Max(value, min);

        public static short ClampMin(short value, short min) => Math.Max(value, min);
        public static ushort ClampMin(ushort value, ushort min) => Math.Max(value, min);

        public static long ClampMin(long value, long min) => Math.Max(value, min);
        public static ulong ClampMin(ulong value, ulong min) => Math.Max(value, min);

        public static int ClampMin(int value, int min) => Math.Max(value, min);
        public static uint ClampMin(uint value, uint min) => Math.Max(value, min);
        #endregion

        #region FloorToInt Implementation
        public static int FloorToInt(decimal value) => (int)Math.Floor(value);
        public static int FloorToInt(float value) => (int)Math.Floor(value);
        public static int FloorToInt(double value) => (int)Math.Floor(value);
        #endregion

        #region FloorToInt Implementation
        public static int CeilToInt(decimal value) => (int)Math.Ceiling(value);
        public static int CeilToInt(float value) => (int)Math.Ceiling(value);
        public static int CeilToInt(double value) => (int)Math.Ceiling(value);
        #endregion

        #region RoundToInt Implementation
        public static int RoundToInt(decimal value) => (int)Math.Round(value);
        public static int RoundToInt(float value) => (int)Math.Round(value);
        public static int RoundToInt(double value) => (int)Math.Round(value);
        #endregion

        #region To1DArray Implementation
        public static T[] To1DArray<T>(this T[,] input)
        {
            return To1DArray(input, i => i);
        }

        public static TResult[] To1DArray<T, TResult>(this T[,] input, Func<T, TResult> converter)
        {
            TResult[] result = new TResult[input.Length];

            int write = 0;
            for (int y = 0; y <= input.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= input.GetUpperBound(0); x++)
                {
                    result[write++] = converter(input[x, y]);
                }
            }

            return result;
        }
        #endregion
    }
}
