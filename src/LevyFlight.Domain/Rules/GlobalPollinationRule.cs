using System.Threading.Tasks;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Entities;
using LevyFlight.Extensions;
using LevyFlight.Logging.Contracts;

namespace LevyFlight.Domain.Rules
{
    internal sealed class GlobalPollinationRule : IRule<GlobalPollinationRuleArgument>
    {
        private readonly ILogger _logger;

        public GlobalPollinationRule(ILogger logger)
        {
            _logger = logger;
        }

        public Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, GlobalPollinationRuleArgument ruleArgument)
        {
            var bestPollinator = ruleArgument.BestPollinator;
            var worstPollinator = ruleArgument.WorstPollinator;

            _logger.Trace($"Best pollinator is {bestPollinator.ToArrayRepresentation()}");
            _logger.Trace($"Worst pollinator is {worstPollinator.ToArrayRepresentation()}");
            _logger.Trace($"Current pollinator is {pollinator.ToArrayRepresentation()}");

            return Task.Run(() =>
            {
                var distanceDifference = 
                    worstPollinator.CountFunction(FunctionStrategies.FunctionStrategies.DistanceFunction) -
                    pollinator.CountFunction(FunctionStrategies.FunctionStrategies.DistanceFunction);

                var lambda = FunctionStrategies.FunctionStrategies.LambdaFunction(distanceDifference);

                var rand = FunctionStrategies.FunctionStrategies.MantegnaFunction(lambda);

                var result = pollinator + rand * (bestPollinator - pollinator);

                _logger.Trace($"Global result pollinator is {result.ToArrayRepresentation()}");

                return result;
            });
        }
    }
}
