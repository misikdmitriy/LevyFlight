using LevyFlight.Common.Check;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.RuleArguments
{
    internal sealed class GlobalPollinationRuleArgument : RuleArgument
    {
        public Pollinator BestPollinator { get; }
        public Pollinator WorstPollinator { get; }

        public GlobalPollinationRuleArgument(Pollinator bestPollinator, Pollinator worstPollinator)
        {
            ExceptionHelper.ThrowExceptionIfNull(bestPollinator, nameof(bestPollinator));
            ExceptionHelper.ThrowExceptionIfNull(worstPollinator, nameof(worstPollinator));

            BestPollinator = bestPollinator;
            WorstPollinator = worstPollinator;
        }
    }
}
