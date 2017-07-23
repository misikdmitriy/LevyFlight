using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Rules
{
    internal sealed class GlobalPollinationRule : IRule<GlobalPollinationRuleArgument>
    {
        public void RecountPollinator(Pollinator pollinator, GlobalPollinationRuleArgument ruleArgument)
        {
            var bestPollinator = ruleArgument.BestPollinator;
            var worstPollinator = ruleArgument.WorstPollinator;

            pollinator.NewSolution = new double[pollinator.Size];

            for (var i = 0; i < pollinator.Size; i++)
            {
                var distanceDifference = FunctionStrategies.FunctionStrategies.DistanceFunction(worstPollinator.CurrentSolution) -
                                         FunctionStrategies.FunctionStrategies.DistanceFunction(pollinator.CurrentSolution);

                var lambda = FunctionStrategies.FunctionStrategies.LambdaFunction(distanceDifference);

                var rand = FunctionStrategies.FunctionStrategies.MantegnaFunction(lambda);

                pollinator.NewSolution[i] = pollinator.CurrentSolution[i] + rand * (bestPollinator.CurrentSolution[i] -
                    pollinator.CurrentSolution[i]);
            }
        }
    }
}
