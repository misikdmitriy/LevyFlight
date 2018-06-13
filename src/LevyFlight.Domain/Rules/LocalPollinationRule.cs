using System.Threading.Tasks;
using LevyFlight.Common.Misc;
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
                var rand = RandomGenerator.Random.NextDouble();

                _logger.Trace($"Local result pollinator is {PollinatorExtensions.ToString(randomPollinator)}");

                return pollinator + rand * randomPollinator - pollinator;
            });
        }
    }
}
