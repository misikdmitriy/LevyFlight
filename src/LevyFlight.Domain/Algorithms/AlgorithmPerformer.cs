using System;
using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Entities;
using LevyFlight.Extensions;
using LevyFlight.Logging.Contracts;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Domain.Algorithms
{
    internal abstract class AlgorithmPerformer
    {
        protected PollinatorsGroup[] Groups { get; }
        protected AlgorithmSettings AlgorithmSettings { get; }
        protected Func<double[], double> FunctionStrategy { get; }

        private readonly ILogger _logger;

        protected AlgorithmPerformer(AlgorithmSettings algorithmSettings, int variablesCount,
            Func<double[], double> functionStrategy, IPollinatorGroupCreator pollinatorGroupCreator, 
            ILogger logger)
        {
            AlgorithmSettings = algorithmSettings;

            Groups = new PollinatorsGroup[algorithmSettings.GroupsCount];

            for (var i = 0; i < algorithmSettings.GroupsCount; i++)
            {
                Groups[i] = pollinatorGroupCreator.Create(algorithmSettings.PollinatorsCount, variablesCount);
            }

            FunctionStrategy = functionStrategy;
            _logger = logger;
        }

        public async Task<Pollinator> PolinateAsync()
        {
            var t = 0;
            var bestSolution = Groups.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);

            while (t++ < AlgorithmSettings.MaxGeneration)
            {
                _logger.Debug($"Start step {t - 1}");
                await PolinateOnceAsync();

                bestSolution = Groups.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);

                _logger.Debug($"Best pollinator after step {t - 1} is {bestSolution.ToArrayRepresentation()}");
                _logger.Debug($"Best solution after step {t - 1} is {bestSolution.CountFunction(FunctionStrategy)}");
            }

            return bestSolution;
        }

        public Task PolinateOnceAsync()
        {
            return Task.Run(async () =>
            {
                foreach (var group in Groups)
                {
                    foreach (var pollinator in group)
                    {
                        await PreOperationActionAsync(group, pollinator);

                        var nextPollinator = RandomGenerator.Random.NextDouble() < AlgorithmSettings.P
                            ? await GoFirstBranchAsync(group, pollinator)
                            : await GoSecondBranchAsync(group, pollinator);

                        await PostOperationActionAsync(group, pollinator, nextPollinator);
                    }
                }
            });
        }

        protected abstract Task PreOperationActionAsync(PollinatorsGroup group, Pollinator curr);
        protected abstract Task<Pollinator> GoFirstBranchAsync(PollinatorsGroup group, Pollinator pollinator);
        protected abstract Task<Pollinator> GoSecondBranchAsync(PollinatorsGroup group, Pollinator pollinator);
        protected abstract Task PostOperationActionAsync(PollinatorsGroup group, Pollinator currentPollinator, Pollinator newPollinator);
    }
}
