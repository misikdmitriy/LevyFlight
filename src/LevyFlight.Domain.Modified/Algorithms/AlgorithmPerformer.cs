using System;
using System.Linq;
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
        private GlobalPollinationRule GlobalPollinationRule { get; }
        private LocalPollinationRule LocalPollinationRule { get; }

        private readonly double _pReset;

        public AlgorithmPerformer(ModifiedAlgorithmSettings algorithmSettings, 
            PollinatorsGroup[] groups,
            Func<double[], double> functionStrategy, 
            GlobalPollinationRule globalPollinationRule, 
            LocalPollinationRule localPollinationRule) 
            : base(algorithmSettings, groups, functionStrategy)
        {
            GlobalPollinationRule = globalPollinationRule;
            LocalPollinationRule = localPollinationRule;

            _pReset = algorithmSettings.PReset;
        }

        public new Pollinator Polinate()
        {
            return base.Polinate();
        }

        protected override void GoFirstBranch(PollinatorsGroup @group, Pollinator pollinator)
        {
            var bestPollinator = group.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);
            var worstPollinator = group.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);

            var ruleArgument = new GlobalPollinationRuleArgument(bestPollinator, worstPollinator);
            GlobalPollinationRule.RecountPollinator(pollinator, ruleArgument);
        }

        protected override void GoSecondBranch(PollinatorsGroup @group, Pollinator pollinator)
        {
            var randomPollinator = group.ElementAt(RandomGenerator.Random.Next() % group.Count());

            var ruleArgument = new LocalPollinationRuleArgument(randomPollinator);
            LocalPollinationRule.RecountPollinator(pollinator, ruleArgument);
        }

        protected override void PostOperationAction(PollinatorsGroup @group, Pollinator pollinator)
        {
            pollinator.ThrowExceptionIfValuesIncorrect();

            if (RandomGenerator.Random.NextDouble() < _pReset)
            {
                var i = RandomGenerator.Random.Next() % pollinator.CurrentSolution.Length;
                pollinator.CurrentSolution[i] = RandomGenerator.Random.NextDouble();
            }
        }
    }
}
