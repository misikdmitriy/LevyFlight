using LevyFlight.Common.Check;
using LevyFlight.Entities;

namespace LevyFlight.Domain.RuleArguments
{
    internal sealed class LocalPollinationRuleArgument
    {
        public Pollinator RandomPollinator { get; }

        public LocalPollinationRuleArgument(Pollinator randomPollinator)
        {
            ThrowIf.Null(randomPollinator, nameof(randomPollinator));

            RandomPollinator = randomPollinator;
        }
    }
}
