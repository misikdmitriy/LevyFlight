using System;

namespace LevyFlight.Strategies
{
    public class LambdaFunctionStrategy : IFunctionStrategy<double, double>
    {
        private const double T = 0.73;
        private const double Y = 1.0;
        private const double Dy = 0.3;

        public double Apply(double arguments)
        {
            return T * Math.Exp(-arguments * Y) + Dy;
        }
    }
}
