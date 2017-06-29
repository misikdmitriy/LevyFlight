using System.Linq;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.Algorithms;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Entities;
using LevyFlight.Strategies;

namespace LevyFlight.Domain.Modified.Algorithms
{
    public class AlgorithmPerformer : AlgorithmPerformer<GlobalPollinationRuleArgument, LocalPollinationRuleArgument>
    {
        private readonly double _pReset;

        public AlgorithmPerformer(ModifiedAlgorithmSettings algorithmSettings, 
            PollinatorsGroup[] groups, 
            IFunctionStrategy functionStrategy, 
            GlobalPollinationRule globalPollinationRule, 
            LocalPollinationRule localPollinationRule) 
            : base(algorithmSettings, groups, functionStrategy, globalPollinationRule, localPollinationRule)
        {
            _pReset = algorithmSettings.PReset;
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
