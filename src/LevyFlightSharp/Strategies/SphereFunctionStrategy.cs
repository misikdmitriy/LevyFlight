using System.Linq;

namespace LevyFlightSharp.Strategies
{
    public class SphereFunctionStrategy : IFunctionStrategy<double, double[]>
    {
        public double Apply(double[] arguments)
        {
            return arguments.Select(a => a * a).Sum();
        }
    }
}
