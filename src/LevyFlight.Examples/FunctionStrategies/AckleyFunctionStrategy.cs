using LevyFlight.Strategies;
using System;

namespace LevyFlight.Examples.FunctionStrategies
{
    public class AckleyFunctionStrategy : IFunctionStrategy
    {
        public double Apply(double[] arguments)
        {
            var sum1 = 0.0;
            var sum2 = 0.0;
            var n = arguments.Length;

            foreach (var argument in arguments)
            {
                sum1 += argument * argument;
                sum2 += Math.Cos(2 * Math.PI * argument);
            }

            sum1 = -20.0 * Math.Exp(-1.0 / 5.0 * Math.Sqrt(1.0 / n * sum1));
            sum2 = Math.Exp(1.0 / n * sum2);

            return sum1 - sum2 + 20 + Math.E;
        }
    }
}
