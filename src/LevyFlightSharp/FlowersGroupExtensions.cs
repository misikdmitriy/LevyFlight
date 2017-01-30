using System;
using System.Linq;

namespace LevyFlightSharp
{
    public static class FlowersGroupExtensions
    {
        public static int IndexOf(this FlowersGroup group, Flower flower)
        {
            return Array.IndexOf(group.Flowers, flower);
        }

        public static Flower FindGlobalBest(this FlowersGroup[] groups, bool isMin = true)
        {
            var best = groups[0].BestSolution;

            foreach (var group in groups.Skip(1))
            {
                if (isMin && group.BestSolution.CountFunction(Solution.Current) < best.CountFunction(Solution.Current)
                    || !isMin && group.BestSolution.CountFunction(Solution.Current) > best.CountFunction(Solution.Current))
                {
                    best = group.BestSolution;
                }
            }

            return best;
        }
    }
}
