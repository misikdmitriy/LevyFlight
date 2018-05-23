using System;

namespace LevyFlight.Common.Check
{
    public class ExceptionHelper
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

        public static void ThrowExceptionIfNotEqual(object obj1, object obj2)
        {
            if (!Equals(obj1, obj2))
            {
                throw new Exception("Objects are not equal");
            }
        }
    }
}
