using LevyFlight.Common.Check;
using LevyFlight.Entities;

namespace LevyFlight.Domain.RuleArguments
{
    public class ResetPollinationRuleArgument
    {
        public PollinatorsGroup Group { get; }
        public Pollinator NewPollinator { get; }

        public ResetPollinationRuleArgument(PollinatorsGroup group, Pollinator newPollinator)
        {
            ThrowIf.Null(group, nameof(group));
            ThrowIf.Null(newPollinator, nameof(newPollinator));

            Group = group;
            NewPollinator = newPollinator;
        }
    }
}
