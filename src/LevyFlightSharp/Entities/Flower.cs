using System;

using LevyFlightSharp.Services;
using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Entities
{
    public class Flower
    {
        private const double Lambda = 1.5;
        private const double P = 0.01;

        private int Size { get; }
        private IFunctionStrategy<double, double[]> FunctionStrategy { get; }
        private IFunctionStrategy<double, double> MantegnaFunctionStrategy { get; }

        private double[] CurrentFlower { get; set; }
        private double[] NewFlower { get; set; }

        public Flower(int size, IFunctionStrategy<double, double[]> mainFunctionStrategy, 
            IFunctionStrategy<double, double> mantegnaFunctionStrategy)
        {
            if (size <= 0)
            {
                throw new ArgumentException(nameof(size));
            }

            FunctionStrategy = mainFunctionStrategy;
            Size = size;
            MantegnaFunctionStrategy = mantegnaFunctionStrategy;

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
                var rand = MantegnaFunctionStrategy.Function(Lambda);

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
                    return FunctionStrategy.Function(CurrentFlower);
                case Solution.NewSolution:
                    return FunctionStrategy.Function(NewFlower);
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
    }

    public enum Solution
    {
        Current,
        NewSolution
    }
}