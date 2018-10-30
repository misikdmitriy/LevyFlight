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
	        if (isMin)
	        {
				return pollinatorsGroups.SelectMany(x => x)
					.OrderBy(x => x.CountFunction(functionStrategy))
					.First();
			}

	        return pollinatorsGroups.SelectMany(x => x)
		        .OrderByDescending(x => x.CountFunction(functionStrategy))
		        .First();
        }

        public static Pollinator GetBestSolution(this PollinatorsGroup pollinatorsGroup,
            Func<double[], double> functionStrategy,
            bool isMin = true)
        {
            return GetBestSolution(new[] {pollinatorsGroup}, functionStrategy, isMin);
        }
    }
}
