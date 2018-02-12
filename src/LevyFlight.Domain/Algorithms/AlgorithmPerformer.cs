using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        protected AlgorithmPerformer(AlgorithmSettings algorithmSettings, int variablesCount, 
            Func<double[], double> functionStrategy)
        {
            AlgorithmSettings = algorithmSettings;

            var groupsList = new List<PollinatorsGroup>();
            for (var i = 0; i < algorithmSettings.GroupsCount; i++)
            {
                groupsList.Add(new PollinatorsGroup(algorithmSettings.PollinatorsCount, variablesCount));
            }
            Groups = groupsList.ToArray();

            FunctionStrategy = functionStrategy;
        }

        public async Task<Pollinator> PolinateAsync()
        {
            var t = 0;

            while (t < AlgorithmSettings.MaxGeneration)
            {
                await PolinateOnceAsync();

                ++t;
            }

            return Groups.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);
        }

        public async Task PolinateOnceAsync()
        {
            await Task.Run(async () =>
            {
                foreach (var group in Groups)
                {
                    foreach (var pollinator in group)
                    {
                        if (RandomGenerator.Random.NextDouble() < AlgorithmSettings.P)
                        {
                            await GoFirstBranchAsync(group, pollinator);
                        }
                        else
                        {
                            await GoSecondBranchAsync(group, pollinator);
                        }

                        await PostOperationActionAsync(group, pollinator);
                    }
                }
            });
        }

        protected abstract Task GoFirstBranchAsync(PollinatorsGroup group, Pollinator pollinator);
        protected abstract Task GoSecondBranchAsync(PollinatorsGroup group, Pollinator pollinator);
        protected abstract Task PostOperationActionAsync(PollinatorsGroup group, Pollinator pollinator);
    }
}
