using System;
using System.Linq;
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

            Groups = Enumerable.Repeat(pollinatorGroupCreator.Create(algorithmSettings.PollinatorsCount, variablesCount),
                algorithmSettings.GroupsCount)
                .ToArray();

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
