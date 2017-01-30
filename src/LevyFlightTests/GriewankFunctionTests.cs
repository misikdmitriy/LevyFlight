using System;

using Xunit;

namespace LevyFlightTests
{
    public class GriewankFunctionTests : LevyFlightTests
    {
        public GriewankFunctionTests()
            : base(GriewankFunction)
        {
        }

        [Theory]
        [InlineData(1000, 0.0, 1e-1)]
        public override void CheckMethod(int steps, double expected, double eps)
        {
            base.CheckMethod(steps, expected, eps);
        }

        private static double GriewankFunction(double[] flowers)
        {
            var sum1 = 0.0;
            var sum2 = 1.0;

            var num = 1;
            foreach (var x in flowers)
            {
                sum1 += x * x / 4000.0;
                sum2 *= Math.Cos(x / Math.Sqrt(num));
                ++num;
            }

            return sum1 - sum2 + 1.0;
        }
    }
}
