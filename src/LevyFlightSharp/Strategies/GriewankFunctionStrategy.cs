using System;

namespace LevyFlightSharp.Strategies
{
    public class GriewankFunctionStrategy : IFunctionStrategy<double, double[]>
    {
        public double Function(double[] arguments)
        {
            var sum1 = 0.0;
            var sum2 = 1.0;

            var num = 1;
            foreach (var x in arguments)
            {
                sum1 += x * x / 4000.0;
                sum2 *= Math.Cos(x / Math.Sqrt(num));
                ++num;
            }

            return sum1 - sum2 + 1.0;
        }
    }
}
