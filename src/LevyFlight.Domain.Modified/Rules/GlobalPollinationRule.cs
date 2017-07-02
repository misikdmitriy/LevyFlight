using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Rules;
using LevyFlight.Entities;
using LevyFlight.FunctionStrategies;

namespace LevyFlight.Domain.Modified.Rules
{
    internal sealed class GlobalPollinationRule : Rule<GlobalPollinationRuleArgument>
    {
        private readonly IFunctionStrategy _distanceFunctionStrategy;
        private readonly IFunctionStrategy _lambdaFunctionStrategy;
        private readonly IFunctionStrategy _mantegnaFunctionStrategy;

        public GlobalPollinationRule(IFunctionStrategy distanceFunctionStrategy,
            IFunctionStrategy lambdaFunctionStrategy,
            IFunctionStrategy mantegnaFunctionStrategy)
        {
            _distanceFunctionStrategy = distanceFunctionStrategy;
            _lambdaFunctionStrategy = lambdaFunctionStrategy;
            _mantegnaFunctionStrategy = mantegnaFunctionStrategy;
        }

        public override void RecountPollinator(Pollinator pollinator, GlobalPollinationRuleArgument ruleArgument)
        {
            var bestPollinator = ruleArgument.BestPollinator;
            var worstPollinator = ruleArgument.WorstPollinator;

            pollinator.NewSolution = new double[pollinator.Size];

            for (var i = 0; i < pollinator.Size; i++)
            {
                var distanceDifference = _distanceFunctionStrategy.Apply(worstPollinator.CurrentSolution) -
                                         _distanceFunctionStrategy.Apply(pollinator.CurrentSolution);

                var lambda = _lambdaFunctionStrategy.Apply(new[] { distanceDifference });

                var rand = _mantegnaFunctionStrategy.Apply(new[] { lambda });

                pollinator.NewSolution[i] = pollinator.CurrentSolution[i] + rand * (bestPollinator.CurrentSolution[i] -
                    pollinator.CurrentSolution[i]);
            }
        }
    }
}
