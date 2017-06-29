using LevyFlight.Common.Check;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.RuleArguments
{
    public class GlobalPollinationRuleArgument : RuleArgument
    {
        public Pollinator BestPollinator { get; }
        public Pollinator WorstPollinator { get; }

        public GlobalPollinationRuleArgument(Pollinator bestPollinator, Pollinator worstPollinator)
        {
            ExceptionHandler.ThrowExceptionIfNull(bestPollinator, nameof(bestPollinator));
            ExceptionHandler.ThrowExceptionIfNull(worstPollinator, nameof(worstPollinator));

            BestPollinator = bestPollinator;
            WorstPollinator = worstPollinator;
        }
    }
}
