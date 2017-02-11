using System.Linq;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Services;

namespace LevyFlightSharp.Algorithms
{
    public class FlowerAnalyzer
    {
        public Flower GetBestSolution(FlowersGroup[] flowersGroups, bool isMin = true)
        {
            var bestSolution = flowersGroups.First().First();
            var best = bestSolution.CountFunction(Solution.Current);

            foreach (var group in flowersGroups)
            {
                foreach (var flower in group)
                {
                    var current = flower.CountFunction(Solution.Current);

                    if (isMin && current < best || !isMin && current > best)
                    {
                        bestSolution = flower;
                        best = current;
                    }
                }
            }

            return bestSolution;
        }

        public Flower GetBestSolution(FlowersGroup flowersGroup, bool isMin = true)
        {
            return GetBestSolution(new[] { flowersGroup }, isMin);
        }
    }
}
