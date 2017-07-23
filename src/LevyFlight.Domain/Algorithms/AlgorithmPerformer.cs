using System;
using LevyFlight.Common.Misc;
using LevyFlight.Entities;
using LevyFlight.Extensions;

namespace LevyFlight.Domain.Algorithms
{
    internal abstract class AlgorithmPerformer
    {
        protected PollinatorsGroup[] Groups { get; }
        protected AlgorithmSettings AlgorithmSettings { get; }
        protected Func<double[], double> FunctionStrategy { get; }

        protected AlgorithmPerformer(AlgorithmSettings algorithmSettings, PollinatorsGroup[] groups, 
            Func<double[], double> functionStrategy)
        {
            AlgorithmSettings = algorithmSettings;
            Groups = groups;
            FunctionStrategy = functionStrategy;
        }

        public Pollinator Polinate()
        {
            var t = 0;

            while (t < AlgorithmSettings.MaxGeneration)
            {
                PolinateOnce();

                ++t;
            }

            return Groups.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);
        }

        private void PolinateOnce()
        {
            foreach (var group in Groups)
            {
                foreach (var pollinator in group)
                {
                    if (RandomGenerator.Random.NextDouble() < AlgorithmSettings.P)
                    {
                        GoFirstBranch(group, pollinator);
                    }
                    else
                    {
                        GoSecondBranch(group, pollinator);
                    }

                    PostOperationAction(group, pollinator);
                }
            }
        }

        protected abstract void GoFirstBranch(PollinatorsGroup group, Pollinator pollinator);
        protected abstract void GoSecondBranch(PollinatorsGroup group, Pollinator pollinator);
        protected abstract void PostOperationAction(PollinatorsGroup group, Pollinator pollinator);
    }
}
