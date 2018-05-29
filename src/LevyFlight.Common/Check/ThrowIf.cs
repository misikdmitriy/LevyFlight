using System;

namespace LevyFlight.Common.Check
{
    public class ThrowIf
    {
        public static void Null(object argument, string nameof)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(nameof);
            }
        }

        public static void NotPositive(int num, string nameof)
        {
            if (num <= 0)
            {
                throw new ArgumentException(nameof);
            }
        }

        public static void NotBetweenZeroAndOne(double num, string nameof)
        {
            if (num < 0.0 || num > 1.0)
            {
                throw new ArgumentException(nameof);
            }
        }

        public static void NotEqual(object obj1, object obj2, string message = null)
        {
            if (!Equals(obj1, obj2))
            {
                throw new Exception(message ?? "Objects are not equal");
            }
        }
    }
}
