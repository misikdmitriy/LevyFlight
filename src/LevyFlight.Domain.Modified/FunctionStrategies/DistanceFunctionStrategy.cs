using System;
using System.Linq;
using LevyFlight.FunctionStrategies;

namespace LevyFlight.Domain.Modified.FunctionStrategies
{
    internal sealed class DistanceFunctionStrategy : IFunctionStrategy
    {
        public double Apply(double[] arguments)
        {
            return Math.Sqrt(arguments.Sum(argument => argument * argument));
        }
    }
}
