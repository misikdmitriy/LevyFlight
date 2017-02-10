using System;
using System.Linq;

namespace LevyFlightSharp.Strategies
{
    public class RastriginFunctionStrategy : IFunctionStrategy<double, double[]>
    {
        public double Function(double[] arguments)
        {
            var a = 10.0;
            var an = arguments.Length * a;

            var sum1 = arguments.Sum(x => x * x - a * Math.Cos(2 * Math.PI * x));

            return an + sum1;
        }
    }
}
