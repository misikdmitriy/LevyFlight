using System;
using System.Numerics;

namespace LevyFlightSharp.Domain
{
    public class Flower
    {
        private const double Lambda = 1.5;
        private const double P = 0.01;

        private int Size { get; }
        private Func<double[], double> Function { get; }

        private double[] CurrentFlower { get; set; }
        private double[] NewFlower { get; set; }

        public Flower(int size, Func<double[], double> function)
        {
            if (size <= 0)
            {
                throw new ArgumentException(nameof(size));
            }

            Function = function;
            Size = size;

            CurrentFlower = new double[Size];
            NewFlower = null;

            for (var i = 0; i < size; i++)
            {
                CurrentFlower[i] = RandomGenerator.Random.NextDouble();
            }
        }

        public void RecountByFirstBranch(Flower solution)
        {
            NewFlower = new double[Size];

            for (var i = 0; i < Size; i++)
            {
                var rand = MantegnaRandom(Lambda);

                NewFlower[i] = CurrentFlower[i] + rand * (solution.CurrentFlower[i] - CurrentFlower[i]);
            }
        }

        public void RecountBySecondBranch(Flower solution)
        {
            NewFlower = new double[Size];

            for (var i = 0; i < Size; i++)
            {
                var rand = RandomGenerator.Random.NextDouble();

                NewFlower[i] = CurrentFlower[i] + rand * (solution.CurrentFlower[i] - CurrentFlower[i]);
            }
        }

        public double CountFunction(Solution solution)
        {
            switch (solution)
            {
                case Solution.Current:
                    return Function(CurrentFlower);
                case Solution.NewSolution:
                    return Function(NewFlower);
                default:
                    throw new ArgumentOutOfRangeException(nameof(solution), solution, null);
            }
        }

        public bool TryExchange(bool isMin = true)
        {
            if (isMin && CountFunction(Solution.Current) > CountFunction(Solution.NewSolution)
                || !isMin && CountFunction(Solution.Current) < CountFunction(Solution.NewSolution))
            {
                CurrentFlower = NewFlower;
                return true;
            }
            if (RandomGenerator.Random.NextDouble() < P)
            {
                var i = RandomGenerator.Random.Next() % CurrentFlower.Length;
                CurrentFlower[i] = RandomGenerator.Random.NextDouble();
                return true;
            }
            return false;
        }

        public void Check()
        {
            foreach (var element in CurrentFlower)
            {
                if (double.IsNaN(element))
                {
                    throw new ArithmeticException("Got NaN");
                }
                if (double.IsInfinity(element))
                {
                    throw new ArithmeticException("Got infinity");
                }
            }
        }

        public string ToString(Solution solution)
        {
            switch (solution)
            {
                case Solution.Current:
                    return ToString(CurrentFlower);
                case Solution.NewSolution:
                    return ToString(NewFlower);
                default:
                    throw new ArgumentOutOfRangeException(nameof(solution), solution, null);
            }
        }

        private string ToString(double[] flower)
        {
            var result = "";
            for (var i = 0; i < Size; i++)
            {
                result += $"{i}): {flower[i]:e2}; ";
            }
            return result;
        }

        private double MantegnaRandom(double lambda)
        {
            Complex sigmaX;
            double x, y;

            sigmaX = Gamma(lambda + 1) * Math.Sin(Math.PI * lambda / 2);
            var divider = Gamma(lambda / 2) * lambda * Math.Pow(2.0, (lambda - 1) / 2);
            sigmaX /= divider;
            sigmaX = Math.Pow(sigmaX.Magnitude, 1.0 / lambda);

            x = GaussianRandom(0, sigmaX.Magnitude);
            y = Math.Abs(GaussianRandom(0, 1.0));

            return x / Math.Pow(y, 1.0 / lambda);
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

    public enum Solution
    {
        Current,
        NewSolution
    }
}