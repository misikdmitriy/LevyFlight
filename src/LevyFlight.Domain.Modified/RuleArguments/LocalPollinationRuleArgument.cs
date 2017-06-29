using LevyFlight.Common.Check;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.RuleArguments
{
    public class LocalPollinationRuleArgument : RuleArgument
    {
        public Pollinator RandomPollinator { get; }

        public LocalPollinationRuleArgument(Pollinator randomPollinator)
        {
            ExceptionHandler.ThrowExceptionIfNull(randomPollinator, nameof(randomPollinator));

            RandomPollinator = randomPollinator;
        }
    }
}
