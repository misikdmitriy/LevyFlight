using System;
using System.Linq;
using System.Threading.Tasks;
using LevyFlight.AsyncHelpers;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Entities;
using LevyFlight.Extensions;

namespace LevyFlight.Domain.Modified.Algorithms
{
    internal sealed class AlgorithmPerformer : Domain.Algorithms.AlgorithmPerformer
    {
        private IRule<GlobalPollinationRuleArgument> GlobalPollinationRule { get; }
        private IRule<LocalPollinationRuleArgument> LocalPollinationRule { get; }

        private readonly TaskBus _taskBus;

        private readonly double _pReset;

        public AlgorithmPerformer(ModifiedAlgorithmSettings algorithmSettings,
            int variablesCount,
            Func<double[], double> functionStrategy,
            IRule<GlobalPollinationRuleArgument> globalPollinationRule,
            IRule<LocalPollinationRuleArgument> localPollinationRule)
            : base(algorithmSettings, variablesCount, functionStrategy)
        {
            GlobalPollinationRule = globalPollinationRule;
            LocalPollinationRule = localPollinationRule;

            _pReset = algorithmSettings.PReset;
            _taskBus = new TaskBus();
        }

        protected override Task GoFirstBranchAsync(PollinatorsGroup @group, Pollinator pollinator)
        {
            var bestPollinator = group.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);
            var worstPollinator = group.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);

            var ruleArgument = new GlobalPollinationRuleArgument(bestPollinator, worstPollinator);
            _taskBus.AddTask(GlobalPollinationRule.ApplyRuleAsync(pollinator, ruleArgument));

            return Task.FromResult(0);
        }

        protected override Task GoSecondBranchAsync(PollinatorsGroup @group, Pollinator pollinator)
        {
            var randomPollinator = group.ElementAt(RandomGenerator.Random.Next() % group.Count());

            var ruleArgument = new LocalPollinationRuleArgument(randomPollinator);
            _taskBus.AddTask(LocalPollinationRule.ApplyRuleAsync(pollinator, ruleArgument));

            return Task.FromResult(0);
        }

        protected override async Task PostOperationActionAsync(PollinatorsGroup @group, Pollinator pollinator)
        {
            await _taskBus.WaitAll();

            pollinator.ThrowExceptionIfValuesIncorrect();

            if (RandomGenerator.Random.NextDouble() < _pReset)
            {
                var i = RandomGenerator.Random.Next() % pollinator.CurrentSolution.Length;
                pollinator.CurrentSolution[i] = RandomGenerator.Random.NextDouble();
            }
        }
    }
}
