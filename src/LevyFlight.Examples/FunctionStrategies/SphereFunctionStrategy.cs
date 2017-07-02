using System.Linq;
using LevyFlight.FunctionStrategies;

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
