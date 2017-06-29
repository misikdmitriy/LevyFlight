using LevyFlight.Entities;

namespace LevyFlight.Domain.Rules
{
    public abstract class Rule<TRuleArgument>
    {
        public abstract void RecountPollinator(Pollinator pollinator, TRuleArgument ruleArgument);
    }
}
