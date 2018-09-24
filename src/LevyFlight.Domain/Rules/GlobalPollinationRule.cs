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

			_logger.Trace($"Best pollinator is {PollinatorExtensions.ToString(bestPollinator)}");
			_logger.Trace($"Current pollinator is {PollinatorExtensions.ToString(pollinator)}");

			return Task.Run(() =>
			{
				var distanceDifference = (pollinator - bestPollinator)
					.CountFunction(FunctionStrategies.FunctionStrategies.DistanceFunction);

				var lambda = FunctionStrategies.FunctionStrategies.LambdaFunction(distanceDifference);

				var rand = FunctionStrategies.FunctionStrategies.MantegnaFunction(lambda);

				var result = pollinator + rand * (bestPollinator - pollinator);

				_logger.Trace($"Global result pollinator is {PollinatorExtensions.ToString(result)}");

				return result;
			});
		}
	}
}
