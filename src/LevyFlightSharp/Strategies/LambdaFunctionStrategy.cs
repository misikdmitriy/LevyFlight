using System;

namespace LevyFlightSharp.Strategies
{
    public class LambdaFunctionStrategy : IFunctionStrategy<double, double>
    {
        private const double T = 0.5;
        private const double Y = 1;

        public double Apply(double arguments)
        {
            return T * Math.Exp(-arguments * Y);
        }
    }
}
