using System;

namespace Ax.Engine.Utils
{
    public static class DefaultValue
    {
        public static void Default<T>(ref T value, Func<T, bool> testPredicate, T defaultValue)
        {
            value = testPredicate(value) ? value : defaultValue;
        }

        public static bool StringNotNullOrEmpty(string v) => !string.IsNullOrEmpty(v);

        public static bool IntegerPositiveOrZero(int n) => n >= 0;

        public static bool IntegerPositive(int n) => n > 0;
    }
}
