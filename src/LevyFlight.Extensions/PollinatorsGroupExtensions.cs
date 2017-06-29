﻿using System;
using System.Linq;
using LevyFlight.Strategies;
using LevyFlight.Entities;

namespace LevyFlight.Domain
{
    public static class PollinatorsGroupExtensions
    {
        public static Pollinator GetBestSolution(this PollinatorsGroup[] pollinatorsGroups, 
            IFunctionStrategy functionStrategy,
            bool isMin = true)
        {
            var bestSolution = pollinatorsGroups.First().First();
            var best = bestSolution.CountFunction(functionStrategy, Solution.Current);

            foreach (var group in pollinatorsGroups)
            {
                foreach (var pollinator in group)
                {
                    var current = pollinator.CountFunction(functionStrategy, Solution.Current);

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
            IFunctionStrategy functionStrategy,
            bool isMin = true)
        {
            return GetBestSolution(new[] {pollinatorsGroup}, functionStrategy, isMin);
        }

        public static int IndexOf(this PollinatorsGroup group, Pollinator pollinator)
        {
            return Array.IndexOf(group.ToArray(), pollinator);
        }
    }
}