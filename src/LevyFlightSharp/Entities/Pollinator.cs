using System;

using LevyFlightSharp.Facade;
using LevyFlightSharp.Services;

namespace LevyFlightSharp.Entities
{
    public class Pollinator
    {
        public const double P = 0.01;

        private int Size { get; }
        private FunctionFacade FunctionFacade { get; }

        private double[] CurrentPollinators { get; set; }
        private double[] NewPollinators { get; set; }

        public Pollinator(int size, FunctionFacade functionFacade)
        {
            if (size <= 0)
            {
                throw new ArgumentException(nameof(size));
            }

            FunctionFacade = functionFacade;
            Size = size;

            CurrentPollinators = new double[Size];
            NewPollinators = null;

            for (var i = 0; i < size; i++)
            {
                CurrentPollinators[i] = RandomGenerator.Random.NextDouble();
            }
        }

        public void RecountByFirstBranch(Pollinator solution1, Pollinator solution2)
        {
            NewPollinators = new double[Size];

            for (var i = 0; i < Size; i++)
            {
                var distanceDifference = FunctionFacade.DistanceFunctionStrategy.Apply(solution2.CurrentPollinators) -
                                         FunctionFacade.DistanceFunctionStrategy.Apply(CurrentPollinators);

                var lambda = FunctionFacade.LambdaFunctionStrategy.Apply(distanceDifference);

                var rand = FunctionFacade.MantegnaFunctionStrategy.Apply(lambda);

                NewPollinators[i] = CurrentPollinators[i] + rand * (solution1.CurrentPollinators[i] - CurrentPollinators[i]);
            }
        }

        public void RecountBySecondBranch(Pollinator solution)
        {
            NewPollinators = new double[Size];

            for (var i = 0; i < Size; i++)
            {
                var rand = RandomGenerator.Random.NextDouble();

                NewPollinators[i] = CurrentPollinators[i] + rand * (solution.CurrentPollinators[i] - CurrentPollinators[i]);
            }
        }

        public double CountFunction(Solution solution)
        {
            switch (solution)
            {
                case Solution.Current:
                    return FunctionFacade.MainFunctionStrategy.Apply(CurrentPollinators);
                case Solution.NewSolution:
                    return FunctionFacade.MainFunctionStrategy.Apply(NewPollinators);
                default:
                    throw new ArgumentOutOfRangeException(nameof(solution), solution, null);
            }
        }

        public bool TryExchange(bool isMin = true)
        {
            if (isMin && CountFunction(Solution.Current) > CountFunction(Solution.NewSolution)
                || !isMin && CountFunction(Solution.Current) < CountFunction(Solution.NewSolution))
            {
                CurrentPollinators = NewPollinators;
                return true;
            }
            if (RandomGenerator.Random.NextDouble() < P)
            {
                var i = RandomGenerator.Random.Next() % CurrentPollinators.Length;
                CurrentPollinators[i] = RandomGenerator.Random.NextDouble();
                return true;
            }
            return false;
        }

        public void Check()
        {
            foreach (var element in CurrentPollinators)
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
                    return ToString(CurrentPollinators);
                case Solution.NewSolution:
                    return ToString(NewPollinators);
                default:
                    throw new ArgumentOutOfRangeException(nameof(solution), solution, null);
            }
        }

        private string ToString(double[] pollinator)
        {
            var result = "";
            for (var i = 0; i < Size; i++)
            {
                result += $"{i}): {pollinator[i]:e2}; ";
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