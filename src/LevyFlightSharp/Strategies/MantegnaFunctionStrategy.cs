using System;
using System.Numerics;

using LevyFlightSharp.Services;

namespace LevyFlightSharp.Strategies
{
    public class MantegnaFunctionStrategy : IFunctionStrategy<double, double>
    {
        public double Apply(double arguments)
        {
            Complex sigmaX;
            double x, y;

            sigmaX = Gamma(arguments + 1) * Math.Sin(Math.PI * arguments / 2);
            var divider = Gamma(arguments / 2) * arguments * Math.Pow(2.0, (arguments - 1) / 2);
            sigmaX /= divider;
            sigmaX = Math.Pow(sigmaX.Magnitude, 1.0 / arguments);

            x = GaussianRandom(0, sigmaX.Magnitude);
            y = Math.Abs(GaussianRandom(0, 1.0));

            return x / Math.Pow(y, 1.0 / arguments);
        }

        private double GaussianRandom(double mue, double sigma)
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

        private Complex Gamma(Complex z)
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
    }
}
