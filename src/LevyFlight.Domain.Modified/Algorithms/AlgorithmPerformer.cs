using System;
using System.Linq;
using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Entities;
using LevyFlight.Extensions;
using LevyFlight.Logging.Contracts;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Domain.Modified.Algorithms
{
    internal sealed class AlgorithmPerformer : Domain.Algorithms.AlgorithmPerformer
    {
        private readonly IRule<GlobalPollinationRuleArgument> _globalPollinationRule;
        private readonly IRule<LocalPollinationRuleArgument> _localPollinationRule;
        private readonly IRule<ResetPollinationRuleArgument> _resetPollinationRule;


        public AlgorithmPerformer(ModifiedAlgorithmSettings algorithmSettings,
            int variablesCount,
            Func<double[], double> functionStrategy,
            IPollinatorGroupCreator pollinatorGroupCreator,
            IRule<GlobalPollinationRuleArgument> globalPollinationRule,
            IRule<LocalPollinationRuleArgument> localPollinationRule, 
            IRule<ResetPollinationRuleArgument> resetPollinationRule,
            ILogger logger)
            : base(algorithmSettings, variablesCount, functionStrategy, pollinatorGroupCreator, logger)
        {
            _globalPollinationRule = globalPollinationRule;
            _localPollinationRule = localPollinationRule;
            _resetPollinationRule = resetPollinationRule;
        }

        protected override Task PreOperationActionAsync(PollinatorsGroup @group, Pollinator curr)
        {
            return Task.CompletedTask;
        }

        protected override Task<Pollinator> GoFirstBranchAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var bestPollinator = group.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);
            var worstPollinator = group.GetBestSolution(FunctionStrategy, !AlgorithmSettings.IsMin);

            var ruleArgument = new GlobalPollinationRuleArgument(bestPollinator, worstPollinator);
            return _globalPollinationRule.ApplyRuleAsync(pollinator, ruleArgument);
        }

        protected override Task<Pollinator> GoSecondBranchAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var randomPollinator = group.ElementAt(RandomGenerator.Random.Next() % group.Count());

            var ruleArgument = new LocalPollinationRuleArgument(randomPollinator);
            return _localPollinationRule.ApplyRuleAsync(pollinator, ruleArgument);
        }

        protected override Task PostOperationActionAsync(PollinatorsGroup group, Pollinator currentPollinator, 
            Pollinator newPollinator)
        {
            var ruleArgument = new ResetPollinationRuleArgument(group, newPollinator);

            return _resetPollinationRule.ApplyRuleAsync(currentPollinator, ruleArgument);
        }
    }
}
