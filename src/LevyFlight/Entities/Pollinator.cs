using System;

using LevyFlight.Facade;
using LevyFlight.Services;

namespace LevyFlight.Entities
{
    public class Pollinator
    {
        public const double P = 0.01;

        private int Size { get; }
        private FunctionFacade FunctionFacade { get; }

        private double[] CurrentSolution { get; set; }
        private double[] NewSolution { get; set; }

        public Pollinator(int size, FunctionFacade functionFacade)
        {
            if (size <= 0)
            {
                throw new ArgumentException(nameof(size));
            }

            FunctionFacade = functionFacade;
            Size = size;

            CurrentSolution = new double[Size];
            NewSolution = null;

            for (var i = 0; i < size; i++)
            {
                CurrentSolution[i] = RandomGenerator.Random.NextDouble();
            }
        }

        public void RecountByFirstBranch(Pollinator solution1, Pollinator solution2)
        {
            NewSolution = new double[Size];

            for (var i = 0; i < Size; i++)
            {
                var distanceDifference = FunctionFacade.DistanceFunctionStrategy.Apply(solution2.CurrentSolution) -
                                         FunctionFacade.DistanceFunctionStrategy.Apply(CurrentSolution);

                var lambda = FunctionFacade.LambdaFunctionStrategy.Apply(distanceDifference);

                var rand = FunctionFacade.MantegnaFunctionStrategy.Apply(lambda);

                NewSolution[i] = CurrentSolution[i] + rand * (solution1.CurrentSolution[i] - CurrentSolution[i]);
            }
        }

        public void RecountBySecondBranch(Pollinator solution)
        {
            NewSolution = new double[Size];

            for (var i = 0; i < Size; i++)
            {
                var rand = RandomGenerator.Random.NextDouble();

                NewSolution[i] = CurrentSolution[i] + rand * (solution.CurrentSolution[i] - CurrentSolution[i]);
            }
        }

        public double CountFunction(Solution solution)
        {
            switch (solution)
            {
                case Solution.Current:
                    return FunctionFacade.MainFunctionStrategy.Apply(CurrentSolution);
                case Solution.NewSolution:
                    return FunctionFacade.MainFunctionStrategy.Apply(NewSolution);
                default:
                    throw new ArgumentOutOfRangeException(nameof(solution), solution, null);
            }
        }

        public bool TryExchange(bool isMin = true)
        {
            if (isMin && CountFunction(Solution.Current) > CountFunction(Solution.NewSolution)
                || !isMin && CountFunction(Solution.Current) < CountFunction(Solution.NewSolution))
            {
                CurrentSolution = NewSolution;
                return true;
            }
            if (RandomGenerator.Random.NextDouble() < P)
            {
                var i = RandomGenerator.Random.Next() % CurrentSolution.Length;
                CurrentSolution[i] = RandomGenerator.Random.NextDouble();
                return true;
            }
            return false;
        }

        public void Check()
        {
            foreach (var element in CurrentSolution)
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
                    return ToString(CurrentSolution);
                case Solution.NewSolution:
                    return ToString(NewSolution);
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