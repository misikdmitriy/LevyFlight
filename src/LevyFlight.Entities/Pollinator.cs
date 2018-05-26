using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevyFlight.Common.Check;

namespace LevyFlight.Entities
{
    public class Pollinator : IEnumerable<double>
    {
        #region Init

        private readonly double[] _values;
        public int Size => _values.Length;

        public Pollinator(IEnumerable<double> values)
        {
            _values = values.ToArray();
        }

        public double this[int index] => _values[index];

        #endregion

        #region Operators

        public static Pollinator operator +(Pollinator first, Pollinator second)
        {
            ExceptionHelper.ThrowExceptionIfNotEqual(first.Size, second.Size);

            var values = new double[first.Size];

            for (var i = 0; i < values.Length; i++)
            {
                values[i] = first._values[i] + second._values[i];
            }

            return new Pollinator(values);
        }

        public static Pollinator operator -(Pollinator first, Pollinator second)
        {
            ExceptionHelper.ThrowExceptionIfNotEqual(first.Size, second.Size);

            var values = new double[first.Size];

            for (var i = 0; i < values.Length; i++)
            {
                values[i] = first._values[i] - second._values[i];
            }

            return new Pollinator(values);
        }

        public static Pollinator operator *(Pollinator pollinator, Pollinator second)
        {
            ExceptionHelper.ThrowExceptionIfNotEqual(pollinator.Size, second.Size);

            var values = new double[pollinator.Size];

            for (var i = 0; i < values.Length; i++)
            {
                values[i] = pollinator._values[i] * second._values[i];
            }

            return new Pollinator(values);
        }

        public static Pollinator operator /(Pollinator pollinator, Pollinator second)
        {
            ExceptionHelper.ThrowExceptionIfNotEqual(pollinator.Size, second.Size);

            var values = new double[pollinator.Size];

            for (var i = 0; i < values.Length; i++)
            {
                values[i] = pollinator._values[i] / second._values[i];
            }

            return new Pollinator(values);
        }

        public static Pollinator operator *(Pollinator pollinator, double num)
        {
            return new Pollinator(pollinator.Select(x => x * num));
        }

        public static Pollinator operator /(Pollinator pollinator, double num)
        {
            return new Pollinator(pollinator.Select(x => x / num));
        }

        public static Pollinator operator *(double num, Pollinator pollinator)
        {
            return pollinator * num;
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<double> GetEnumerator()
        {
            var enumerator = _values.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return (double)enumerator.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Equals

        public override bool Equals(object obj)
        {
            var pollinator = obj as Pollinator;

            if (pollinator == null)
            {
                return false;
            }

            return _values.SequenceEqual(pollinator._values);
        }

        public override int GetHashCode()
        {
            return _values.GetHashCode();
        }

        #endregion
    }
}