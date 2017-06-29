using System;
using System.Linq;

namespace LevyFlight.Strategies
{
    public sealed class DistanceFunctionStrategy : IFunctionStrategy
    {
        public double Apply(double[] arguments)
        {
            return Math.Sqrt(arguments.Sum(argument => argument * argument));
        }
    }
}
