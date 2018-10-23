using System;
using System.Linq;
using LevyFlight.Common.Misc;

namespace LevyFlight.Examples.FunctionStrategies
{
    internal class MultifunctionStrategy
    {
        private readonly Func<double[], double>[] _functors;
        private readonly double[] _weights;

        private int Count => _functors.Length;

        public MultifunctionStrategy(params Func<double[], double>[] functors)
        {
            _functors = functors;
            _weights = new double[Count];

            var random = RandomGenerator.Random;

            for (var i = 0; i < Count; i++)
            {
	            while (Math.Abs(_weights[i]) <= 1e-5)
	            {
		            _weights[i] = random.NextDouble();
				}
			}

            var k = 1 / _weights.Sum();

            for (var i = 0; i < Count; i++)
            {
                _weights[i] *= k;
            }
        }

        public static implicit operator Func<double[], double>(MultifunctionStrategy strategy)
        {
            return doubles =>
            {
                var sum = 0.0;
                for (var i = 0; i < strategy.Count; i++)
                {
                    sum += strategy._weights[i] * strategy._functors[i](doubles);
                }

                return sum;
            };
        } 
    }
}
