using System;
using System.Linq;
using LevyFlight.FunctionStrategies;

namespace LevyFlight.Examples.FunctionStrategies
{
    public class RastriginFunctionStrategy : IFunctionStrategy
    {
        public double Apply(double[] arguments)
        {
            var a = 10.0;
            var an = arguments.Length * a;

            var sum1 = arguments.Sum(x => x * x - a * Math.Cos(2 * Math.PI * x));

            return an + sum1;
        }
    }
}
