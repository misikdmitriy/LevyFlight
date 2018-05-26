using System;
using System.Linq;
using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Entities;
using LevyFlight.Extensions;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Domain.Modified.Algorithms
{
    internal sealed class AlgorithmPerformer : Domain.Algorithms.AlgorithmPerformer
    {
        private readonly IRule<GlobalPollinationRuleArgument> _globalPollinationRule;
        private readonly IRule<LocalPollinationRuleArgument> _localPollinationRule;
        
        private readonly IPollinatorUpdater _pollinatorUpdater;

        private readonly double _pReset;

        public AlgorithmPerformer(ModifiedAlgorithmSettings algorithmSettings,
            int variablesCount,
            Func<double[], double> functionStrategy,
            IPollinatorGroupCreator pollinatorGroupCreator,
            IPollinatorUpdater pollinatorUpdater,
            IRule<GlobalPollinationRuleArgument> globalPollinationRule,
            IRule<LocalPollinationRuleArgument> localPollinationRule)
            : base(algorithmSettings, variablesCount, functionStrategy, pollinatorGroupCreator)
        {
            _globalPollinationRule = globalPollinationRule;
            _localPollinationRule = localPollinationRule;

            _pReset = algorithmSettings.PReset;

            _pollinatorUpdater = pollinatorUpdater;
        }

        protected override Task PreOperationActionAsync(PollinatorsGroup @group, Pollinator curr)
        {
            return Task.CompletedTask;
        }

        protected override Task<Pollinator> GoFirstBranchAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var bestPollinator = group.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);
            var worstPollinator = group.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);

            var ruleArgument = new GlobalPollinationRuleArgument(bestPollinator, worstPollinator);
            return _globalPollinationRule.ApplyRuleAsync(pollinator, ruleArgument);
        }

        protected override Task<Pollinator> GoSecondBranchAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var randomPollinator = group.ElementAt(RandomGenerator.Random.Next() % group.Count());

            var ruleArgument = new LocalPollinationRuleArgument(randomPollinator);
            return _localPollinationRule.ApplyRuleAsync(pollinator, ruleArgument);
        }

        protected override Task PostOperationActionAsync(PollinatorsGroup group, Pollinator prev, Pollinator curr)
        {
            if (!curr.CheckWhetherValuesCorrect())
            {
                return Task.CompletedTask;
            }

            Pollinator best;

            if (prev.CompareTo(curr, FunctionStrategy) > -1 && AlgorithmSettings.IsMin || 
                prev.CompareTo(curr, FunctionStrategy) < 1 && !AlgorithmSettings.IsMin)
            {
                group.Replace(prev, curr);
                best = curr;
            }
            else
            {
                best = prev;
            }

            if (RandomGenerator.Random.NextDouble() < _pReset)
            {
                group.Replace(best, _pollinatorUpdater.Update(best));
            }

            return Task.CompletedTask;
        }
    }
}
