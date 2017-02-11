using System;

using LevyFlightSharp.Facade;
using LevyFlightSharp.Services;

namespace LevyFlightSharp.Entities
{
    public class Flower
    {
        private FlowerSettings FlowerSettings { get; } = new FlowerSettings(1.5, 0.01);

        private int Size { get; }
        private FunctionFacade FunctionFacade { get; }

        private double[] CurrentFlower { get; set; }
        private double[] NewFlower { get; set; }

        public Flower(int size, FunctionFacade functionFacade)
        {
            if (size <= 0)
            {
                throw new ArgumentException(nameof(size));
            }

            FunctionFacade = functionFacade;
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
                var rand = FunctionFacade.MantegnaFunctionStrategy.Function(FlowerSettings.Lambda);

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
                    return FunctionFacade.MainFunctionStrategy.Function(CurrentFlower);
                case Solution.NewSolution:
                    return FunctionFacade.MainFunctionStrategy.Function(NewFlower);
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
            if (RandomGenerator.Random.NextDouble() < FlowerSettings.P)
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