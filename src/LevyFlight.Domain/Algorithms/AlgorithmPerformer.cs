using System;
using System.Collections.Generic;
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
        public event OnStepFinished StepFinished;

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

            while (++t <= _algorithmSettings.MaxGeneration)
            {
                _logger.Debug($"Start step {t}");
                await PolinateOnceAsync();

                bestSolution = _groups.GetBestSolution(_functionStrategy, _algorithmSettings.IsMin);

                OnStepFinished(new StepFinishedArgs(bestSolution, t));

                _logger.Debug($"Best pollinator after step {t} is {PollinatorExtensions.ToString(bestSolution)}");
                _logger.Debug($"Best solution after step {t} is {bestSolution.CountFunction(_functionStrategy)}");
            }

            return bestSolution;
        }

        public Task PolinateOnceAsync()
        {
            var result = new List<Task>();

            foreach (var group in _groups)
            {
                var groupTasks = group.Select(pollinator => PolinateOnceAsync(group, pollinator));

                var continuation = Task.WhenAll(groupTasks).ContinueWith(async task =>
                {
                    for (var i = 0; i < task.Result.Length; i++)
                    {
                        await PostOperationActionAsync(group, group.ElementAt(i), task.Result[i]);
                    }
                }).Unwrap();

                result.Add(continuation);
            }

            return Task.WhenAll(result);
        }

        public Task<Pollinator> PolinateOnceAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var random = RandomGenerator.Random;

            if (random.NextDouble() < _algorithmSettings.P)
            {
                return GlobalPollinationAsync(group, pollinator);
            }

            return LocalPollinationAsync(group, pollinator);
        }

        private Task<Pollinator> GlobalPollinationAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var bestPollinator = group.GetBestSolution(_functionStrategy, _algorithmSettings.IsMin);

            var ruleArgument = new GlobalPollinationRuleArgument(bestPollinator);
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

        private void OnStepFinished(StepFinishedArgs args)
        {
            StepFinished?.Invoke(this, args);
        }
    }
}
