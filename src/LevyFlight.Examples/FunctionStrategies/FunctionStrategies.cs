using System;
using System.Linq;

namespace LevyFlight.Examples.FunctionStrategies
{
    public static class FunctionStrategies
    {
        #region Griewank function

        public static Func<double[], double> GriewankFunction = arguments =>
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
        };

        #endregion

        #region Ackley function

        public static Func<double[], double> AckleyFunctionStrategy = arguments =>
        {
            var sum1 = 0.0;
            var sum2 = 0.0;
            var n = arguments.Length;

            foreach (var argument in arguments)
            {
                sum1 += argument * argument;
                sum2 += Math.Cos(2 * Math.PI * argument);
            }

            sum1 = -20.0 * Math.Exp(-1.0 / 5.0 * Math.Sqrt(1.0 / n * sum1));
            sum2 = Math.Exp(1.0 / n * sum2);

            return sum1 - sum2 + 20 + Math.E;
        };

        #endregion

        #region Rastrigin function

        public static Func<double[], double> RastriginFunction = arguments =>
        {
            var a = 10.0;
            var an = arguments.Length * a;

            var sum1 = arguments.Sum(x => x * x - a * Math.Cos(2 * Math.PI * x));

            return an + sum1;
        };

        #endregion

        #region Rosenbrock function

        public static Func<double[], double> RosenbrockFunction = arguments =>
        {
            var sum = 0.0;

            for (var i = 0; i < arguments.Length - 1; i++)
            {
                sum += (1 - arguments[i]) * (1 - arguments[i]) +
                       100 * (arguments[i + 1] - arguments[i] * arguments[i]) *
                             (arguments[i + 1] - arguments[i] * arguments[i]);
            }

            return sum;
        };

        #endregion

        #region Sphere function

        public static Func<double[], double> SphereFunction = arguments =>
        {
            return arguments.Select(a => a * a).Sum();
        };

        #endregion
    }
}
