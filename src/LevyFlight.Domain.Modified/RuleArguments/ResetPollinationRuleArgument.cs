using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.RuleArguments
{
    public class ResetPollinationRuleArgument
    {
        public PollinatorsGroup Group { get; }
        public Pollinator NewPollinator { get; }

        public ResetPollinationRuleArgument(PollinatorsGroup group, Pollinator newPollinator)
        {
            Group = group;
            NewPollinator = newPollinator;
        }
    }
}
