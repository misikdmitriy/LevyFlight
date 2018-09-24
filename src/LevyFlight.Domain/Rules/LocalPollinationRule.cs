using System;
using System.Threading.Tasks;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Entities;
using LevyFlight.Extensions;
using LevyFlight.Logging.Contracts;

namespace LevyFlight.Domain.Rules
{
	internal sealed class LocalPollinationRule : IRule<LocalPollinationRuleArgument>
	{
		private readonly ILogger _logger;

		public LocalPollinationRule(ILogger logger)
		{
			_logger = logger;
		}

		public Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, LocalPollinationRuleArgument ruleArgument)
		{
			var randomPollinator = ruleArgument.RandomPollinator;

			_logger.Trace($"Random pollinator is {PollinatorExtensions.ToString(randomPollinator)}");
			_logger.Trace($"Current pollinator is {PollinatorExtensions.ToString(pollinator)}");

			return Task.Run(() =>
			{
				var random = new Random((int)DateTime.Now.Ticks);
				var rand = random.NextDouble();

				var result = pollinator + rand * randomPollinator - pollinator;

				_logger.Trace($"Global result pollinator is {PollinatorExtensions.ToString(result)}");

				return result;
			});
		}
	}
}
