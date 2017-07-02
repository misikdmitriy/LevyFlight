using LevyFlight.Common.Check;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.RuleArguments
{
    internal sealed class LocalPollinationRuleArgument : RuleArgument
    {
        public Pollinator RandomPollinator { get; }

        public LocalPollinationRuleArgument(Pollinator randomPollinator)
        {
            ExceptionHelper.ThrowExceptionIfNull(randomPollinator, nameof(randomPollinator));

            RandomPollinator = randomPollinator;
        }
    }
}
