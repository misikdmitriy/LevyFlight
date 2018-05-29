using System;
using System.Linq;
using System.Numerics;
using LevyFlight.Common.Misc;

namespace LevyFlight.Domain.FunctionStrategies
{
    internal static class FunctionStrategies
    {
        #region Distance function

        public static Func<double[], double> DistanceFunction = arguments =>
        {
            return Math.Sqrt(arguments.Sum(argument => argument * argument));
        };

        #endregion

        #region Lambda function

        public static Func<double, double> LambdaFunction = argument =>
        {
            const double t = 0.73;
            const double y = 1.0;
            const double dy = 0.3;

            return t * Math.Exp(-argument * y) + dy;
        };

        #endregion

        #region Mantegna function

        public static Func<double, double> MantegnaFunction = argument =>
        {
            Complex sigmaX;
            double x, y;

            sigmaX = Gamma(argument + 1) * Math.Sin(Math.PI * argument / 2);
            var divider = Gamma(argument / 2) * argument * Math.Pow(2.0, (argument - 1) / 2);
            sigmaX /= divider;
            sigmaX = Math.Pow(sigmaX.Magnitude, 1.0 / argument);

            x = GaussianRandom(0, sigmaX.Magnitude);
            y = Math.Abs(GaussianRandom(0, 1.0));

            return x / Math.Pow(y, 1.0 / argument);
        };

        #endregion

        #region Private methods

        private static Complex Gamma(Complex z)
        {
            if (z.Real < 0.5)
            {
                return Math.PI / (Complex.Sin(Math.PI * z) * Gamma(1 - z));
            }

            var g = 7;
            double[] p = { 0.99999999999980993, 676.5203681218851, -1259.1392167224028,
                 771.32342877765313, -176.61502916214059, 12.507343278686905,
                 -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7 };

            z -= 1;
            Complex x = p[0];
            for (var i = 1; i < g + 2; i++)
            {
                x += p[i] / (z + i);
            }
            var t = z + g + 0.5;
            return Complex.Sqrt(2 * Math.PI) * (Complex.Pow(t, z + 0.5)) * Complex.Exp(-t) * x;
        }

        private static double GaussianRandom(double mue, double sigma)
        {
            double x1;
            double w, y;

            do
            {
                x1 = 2.0 * RandomGenerator.Random.NextDouble() - 1.0;
                var x2 = 2.0 * RandomGenerator.Random.NextDouble() - 1.0;
                w = x1 * x1 + x2 * x2;
            } while (w >= 1.0);

            var llog = Math.Log(w, Math.E);
            w = Math.Sqrt(-2.0 * llog / w);
            y = x1 * w;

            return mue + sigma * y;
        }

        #endregion
    }
}
