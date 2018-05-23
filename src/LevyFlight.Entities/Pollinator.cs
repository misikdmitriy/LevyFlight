using System.Collections.Generic;
using System.Linq;

namespace LevyFlight.Entities
{
    public class Pollinator
    {
        internal readonly double[] Values;
        public int Size => Values.Length;

        public Pollinator(IEnumerable<double> values)
        {
            Values = values.ToArray();
        }

        public Pollinator Clone()
        {
            return new Pollinator(Values);
        }
    }
}