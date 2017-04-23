using System.Linq;

using LevyFlight.Entities;
using LevyFlight.Services;

namespace LevyFlight.Algorithms
{
    public class PollinatorAnalyzer
    {
        public Pollinator GetBestSolution(PollinatorsGroup[] pollinatorsGroups, bool isMin = true)
        {
            var bestSolution = pollinatorsGroups.First().First();
            var best = bestSolution.CountFunction(Solution.Current);

            foreach (var group in pollinatorsGroups)
            {
                foreach (var pollinator in group)
                {
                    var current = pollinator.CountFunction(Solution.Current);

                    if (isMin && current < best || !isMin && current > best)
                    {
                        bestSolution = pollinator;
                        best = current;
                    }
                }
            }

            return bestSolution;
        }

        public Pollinator GetBestSolution(PollinatorsGroup pollinatorsGroup, bool isMin = true)
        {
            return GetBestSolution(new[] { pollinatorsGroup }, isMin);
        }
    }
}
