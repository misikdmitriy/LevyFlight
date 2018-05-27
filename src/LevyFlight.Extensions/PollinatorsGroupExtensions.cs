using System;
using System.Linq;
using LevyFlight.Entities;

namespace LevyFlight.Extensions
{
    public static class PollinatorsGroupExtensions
    {
        public static Pollinator GetBestSolution(this PollinatorsGroup[] pollinatorsGroups, 
            Func<double[], double> functionStrategy,
            bool isMin = true)
        {
            var bestSolution = pollinatorsGroups.First().First();
            var best = bestSolution.CountFunction(functionStrategy);

            foreach (var group in pollinatorsGroups)
            {
                foreach (var pollinator in group)
                {
                    var current = pollinator.CountFunction(functionStrategy);

                    if (isMin && current < best || !isMin && current > best)
                    {
                        bestSolution = pollinator;
                        best = current;
                    }
                }
            }

            return bestSolution;
        }

        public static Pollinator GetBestSolution(this PollinatorsGroup pollinatorsGroup,
            Func<double[], double> functionStrategy,
            bool isMin = true)
        {
            return GetBestSolution(new[] {pollinatorsGroup}, functionStrategy, isMin);
        }
    }
}
