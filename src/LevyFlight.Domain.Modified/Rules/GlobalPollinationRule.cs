using System.Threading.Tasks;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Entities;
using LevyFlight.Extensions;

namespace LevyFlight.Domain.Modified.Rules
{
    internal sealed class GlobalPollinationRule : IRule<GlobalPollinationRuleArgument>
    {
        public Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, GlobalPollinationRuleArgument ruleArgument)
        {
            var bestPollinator = ruleArgument.BestPollinator;
            var worstPollinator = ruleArgument.WorstPollinator;

            return Task.Run(() =>
            {
                var distanceDifference = 
                    worstPollinator.CountFunction(FunctionStrategies.FunctionStrategies.DistanceFunction) -
                    pollinator.CountFunction(FunctionStrategies.FunctionStrategies.DistanceFunction);

                var lambda = FunctionStrategies.FunctionStrategies.LambdaFunction(distanceDifference);

                var rand = FunctionStrategies.FunctionStrategies.MantegnaFunction(lambda);

                return pollinator + rand * (bestPollinator - pollinator);
            });
        }
    }
}
