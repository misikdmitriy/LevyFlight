using System;
using System.Linq;

using Xunit;

namespace LevyFlightTests
{
    public class RastriginFunctionTests : LevyFlightTests
    {
        public RastriginFunctionTests()
            : base(RastriginFunction)
        {
        }

        [Theory]
        [InlineData(0, 0.0, 1e-1)]
        [InlineData(0, 0.0, 1e-2)]
        [InlineData(0, 0.0, 1e-3)]
        public override void CheckMethod(int steps, double expected, double eps)
        {
            base.CheckMethod(steps, expected, eps);
        }

        private static double RastriginFunction(double[] flowers)
        {
            var a = 10.0;
            var an = flowers.Length * a;

            var sum1 = flowers.Sum(x => x * x - a * Math.Cos(2 * Math.PI * x));

            return an + sum1;
        }
    }
}
