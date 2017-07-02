using System;
using LevyFlight.FunctionStrategies;

namespace LevyFlight.Business
{
    internal class MainFunction : IFunctionStrategy
    {
        private readonly Func<double[], double> _function;

        public MainFunction(Func<double[], double> function)
        {
            _function = function;
        }

        public static IFunctionStrategy CreateFromDelegate(Func<double[], double> function)
        {
            return new MainFunction(function);
        }

        public double Apply(double[] arguments)
        {
            return _function(arguments);
        }
    }
}
