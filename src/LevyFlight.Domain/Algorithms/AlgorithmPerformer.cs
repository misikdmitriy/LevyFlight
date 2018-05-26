using System;
using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Entities;
using LevyFlight.Extensions;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Domain.Algorithms
{
    internal abstract class AlgorithmPerformer
    {
        protected PollinatorsGroup[] Groups { get; }
        protected AlgorithmSettings AlgorithmSettings { get; }
        protected Func<double[], double> FunctionStrategy { get; }

        protected AlgorithmPerformer(AlgorithmSettings algorithmSettings, int variablesCount,
            Func<double[], double> functionStrategy, IPollinatorGroupCreator pollinatorGroupCreator)
        {
            AlgorithmSettings = algorithmSettings;

            Groups = new PollinatorsGroup[algorithmSettings.GroupsCount];

            for (var i = 0; i < algorithmSettings.GroupsCount; i++)
            {
                Groups[i] = pollinatorGroupCreator.Create(algorithmSettings.PollinatorsCount, variablesCount);
            }

            FunctionStrategy = functionStrategy;
        }

        public async Task<Pollinator> PolinateAsync()
        {
            var t = 0;

            while (t++ < AlgorithmSettings.MaxGeneration)
            {
                await PolinateOnceAsync();
            }

            return Groups.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);
        }

        public Task PolinateOnceAsync()
        {
            return Task.Run(async () =>
            {
                foreach (var group in Groups)
                {
                    foreach (var pollinator in group)
                    {
                        var nextPollinator = RandomGenerator.Random.NextDouble() < AlgorithmSettings.P
                            ? await GoFirstBranchAsync(group, pollinator)
                            : await GoSecondBranchAsync(group, pollinator);

                        await PostOperationActionAsync(group, pollinator, nextPollinator);
                    }
                }
            });
        }

        protected abstract Task<Pollinator> GoFirstBranchAsync(PollinatorsGroup group, Pollinator pollinator);
        protected abstract Task<Pollinator> GoSecondBranchAsync(PollinatorsGroup group, Pollinator pollinator);
        protected abstract Task PostOperationActionAsync(PollinatorsGroup group, Pollinator prev, Pollinator curr);
    }
}
