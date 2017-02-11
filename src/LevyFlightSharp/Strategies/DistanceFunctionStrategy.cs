using System;
using System.Linq;

namespace LevyFlightSharp.Strategies
{
    public class DistanceFunctionStrategy : IFunctionStrategy<double, double[]>
    {
        public double Apply(double[] arguments)
        {
            return Math.Sqrt(arguments.Sum(argument => argument * argument));
        }
    }
}
