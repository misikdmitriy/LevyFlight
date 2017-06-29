using LevyFlight.Entities;

namespace LevyFlight.Domain.Rules
{
    public interface IRule<in TRuleArgument>
    {
        void RecountPollinator(Pollinator pollinator, TRuleArgument ruleArgument);
    }
}
