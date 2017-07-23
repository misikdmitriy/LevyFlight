using System;
using LevyFlight.Entities;

namespace LevyFlight.Extensions
{
    public static class PollinatorExtensions
    {
        public static double CountFunction(this Pollinator pollinator, Func<double[], double> functionStrategy, 
            Solution solution)
        {
            switch (solution)
            {
                case Solution.Current:
                    return functionStrategy(pollinator.CurrentSolution);
                case Solution.NewSolution:
                    return functionStrategy(pollinator.NewSolution);
                default:
                    throw new ArgumentOutOfRangeException(nameof(solution), solution, null);
            }
        }

        public static void ThrowExceptionIfValuesIncorrect(this Pollinator pollinator)
        {
            foreach (var element in pollinator.CurrentSolution)
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

        public static bool TryExchange(this Pollinator pollinator, Func<double[], double> functionStrategy,
            bool isMin = true)
        {
            if (isMin && pollinator.CountFunction(functionStrategy, Solution.Current) > pollinator.CountFunction(functionStrategy, Solution.NewSolution)
                || !isMin && pollinator.CountFunction(functionStrategy, Solution.Current) < pollinator.CountFunction(functionStrategy, Solution.NewSolution))
            {
                pollinator.CurrentSolution = pollinator.NewSolution;
                return true;
            }
                
            return false;
        }
    }
}
