using LevyFlight.Common.Check;
using LevyFlight.Entities;

namespace LevyFlight.Domain.RuleArguments
{
    internal sealed class GlobalPollinationRuleArgument
    {
        public Pollinator BestPollinator { get; }
        public Pollinator WorstPollinator { get; }

        public GlobalPollinationRuleArgument(Pollinator bestPollinator, Pollinator worstPollinator)
        {
            ThrowIf.Null(bestPollinator, nameof(bestPollinator));
            ThrowIf.Null(worstPollinator, nameof(worstPollinator));

            BestPollinator = bestPollinator;
            WorstPollinator = worstPollinator;
        }
    }
}
