using LevyFlight.Strategies;

namespace LevyFlight.Examples.FunctionStrategies
{
    public class RosenbrockFunctionStrategy : IFunctionStrategy
    {
        public double Apply(double[] arguments)
        {
            var sum = 0.0;

            for (var i = 0; i < arguments.Length - 1; i++)
            {
                sum += (arguments[i] - 1) * (arguments[i] - 1) +
                       100 * (arguments[i + 1] - arguments[i] * arguments[i]) *
                       (arguments[i + 1] - arguments[i] * arguments[i]);
            }

            return sum;
        }
    }
}
