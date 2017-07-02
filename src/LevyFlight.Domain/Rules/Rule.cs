using LevyFlight.Entities;

namespace LevyFlight.Domain.Rules
{
    internal abstract class Rule<TRuleArgument>
    {
        public abstract void RecountPollinator(Pollinator pollinator, TRuleArgument ruleArgument);
    }
}
