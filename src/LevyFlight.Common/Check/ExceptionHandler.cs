using System;

namespace LevyFlight.Common.Check
{
    public class ExceptionHandler
    {
        public static void ThrowExceptionIfNull(object argument, string @nameof)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(nameof);
            }
        }

        public static void ThrowExceptionIfNegativeOrZero(int number, string @nameof)
        {
            if (number <= 0)
            {
                throw new ArgumentException(nameof);
            }
        }

        public static TType Convert<TType>(object obj) where TType : class
        {
            var result = obj as TType;

            if (result == null)
            {
                throw new InvalidCastException($"Cannot convert from {obj.GetType().Name} to {typeof(TType).Name}");
            }

            return result;
        }
    }
}
