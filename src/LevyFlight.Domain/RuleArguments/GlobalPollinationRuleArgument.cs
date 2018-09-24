using LevyFlight.Common.Check;
using LevyFlight.Entities;

namespace LevyFlight.Domain.RuleArguments
{
    internal sealed class GlobalPollinationRuleArgument
    {
        public Pollinator BestPollinator { get; }

        public GlobalPollinationRuleArgument(Pollinator bestPollinator)
        {
            ThrowIf.Null(bestPollinator, nameof(bestPollinator));

            BestPollinator = bestPollinator;
        }
    }
}
