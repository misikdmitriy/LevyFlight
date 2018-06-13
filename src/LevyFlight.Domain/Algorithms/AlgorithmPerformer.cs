using System;
using System.Linq;
using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.Contracts;
using LevyFlight.Domain.Entities;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Domain.Rules;
using LevyFlight.Entities;
using LevyFlight.Extensions;
using LevyFlight.Logging.Contracts;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Domain.Algorithms
{
    internal sealed class AlgorithmPerformer : IAlgorithmPerformer
    {
        private readonly PollinatorsGroup[] _groups;
        private readonly AlgorithmSettings _algorithmSettings;
        private readonly Func<double[], double> _functionStrategy;

        private readonly ILogger _logger;

        private readonly IRule<GlobalPollinationRuleArgument> _globalPollinationRule;
        private readonly IRule<LocalPollinationRuleArgument> _localPollinationRule;
        private readonly IRule<ResetPollinationRuleArgument> _resetPollinationRule;

        public AlgorithmPerformer(AlgorithmSettings algorithmSettings,
            int variablesCount,
            Func<double[], double> functionStrategy,
            IPollinatorGroupCreator pollinatorGroupCreator,
            IRule<GlobalPollinationRuleArgument> globalPollinationRule,
            IRule<LocalPollinationRuleArgument> localPollinationRule, 
            IRule<ResetPollinationRuleArgument> resetPollinationRule,
            ILogger logger)
        {
            _algorithmSettings = algorithmSettings;

            _groups = new PollinatorsGroup[algorithmSettings.GroupsCount];

            for (var i = 0; i < algorithmSettings.GroupsCount; i++)
            {
                _groups[i] = pollinatorGroupCreator.Create(algorithmSettings.PollinatorsCount, variablesCount);
            }

            _functionStrategy = functionStrategy;
            _logger = logger;

            _globalPollinationRule = globalPollinationRule;
            _localPollinationRule = localPollinationRule;
            _resetPollinationRule = resetPollinationRule;
        }

        public async Task<Pollinator> PolinateAsync()
        {
            var t = 0;
            var bestSolution = _groups.GetBestSolution(_functionStrategy, _algorithmSettings.IsMin);

            while (t++ < _algorithmSettings.MaxGeneration)
            {
                _logger.Debug($"Start step {t}");
                await PolinateOnceAsync();

                bestSolution = _groups.GetBestSolution(_functionStrategy, _algorithmSettings.IsMin);

                _logger.Debug($"Best pollinator after step {t} is {PollinatorExtensions.ToString(bestSolution)}");
                _logger.Debug($"Best solution after step {t} is {bestSolution.CountFunction(_functionStrategy)}");
            }

            return bestSolution;
        }

        public Task PolinateOnceAsync()
        {
            return Task.Run(async () =>
            {
                foreach (var group in _groups)
                {
                    foreach (var pollinator in group)
                    {
                        var nextPollinator = RandomGenerator.Random.NextDouble() < _algorithmSettings.P
                            ? await GlobalPollinationAsync(group, pollinator)
                            : await LocalPollinationAsync(group, pollinator);

                        await PostOperationActionAsync(group, pollinator, nextPollinator);
                    }
                }
            });
        }

        private Task<Pollinator> GlobalPollinationAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var bestPollinator = group.GetBestSolution(_functionStrategy, _algorithmSettings.IsMin);
            var worstPollinator = group.GetBestSolution(_functionStrategy, !_algorithmSettings.IsMin);

            var ruleArgument = new GlobalPollinationRuleArgument(bestPollinator, worstPollinator);
            return _globalPollinationRule.ApplyRuleAsync(pollinator, ruleArgument);
        }

        private Task<Pollinator> LocalPollinationAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var randomPollinator = group.ElementAt(RandomGenerator.Random.Next() % group.Count());

            var ruleArgument = new LocalPollinationRuleArgument(randomPollinator);
            return _localPollinationRule.ApplyRuleAsync(pollinator, ruleArgument);
        }

        private Task PostOperationActionAsync(PollinatorsGroup group, Pollinator currentPollinator, 
            Pollinator newPollinator)
        {
            var ruleArgument = new ResetPollinationRuleArgument(group, newPollinator);

            return _resetPollinationRule.ApplyRuleAsync(currentPollinator, ruleArgument);
        }
    }
}
