using LevyFlight.Strategies;
using System.Linq;

namespace LevyFlight.Examples.FunctionStrategies
{
    public class SphereFunctionStrategy : IFunctionStrategy
    {
        public double Apply(double[] arguments)
        {
            return arguments.Select(a => a * a).Sum();
        }
    }
}
