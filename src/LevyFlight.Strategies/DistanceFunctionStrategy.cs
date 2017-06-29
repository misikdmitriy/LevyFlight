using System;
using System.Linq;

namespace LevyFlight.Strategies
{
    public class DistanceFunctionStrategy : IFunctionStrategy
    {
        public double Apply(double[] arguments)
        {
            return Math.Sqrt(arguments.Sum(argument => argument * argument));
        }
    }
}
