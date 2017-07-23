using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Rules
{
    public interface IRule<in TRuleArgument>
    {
        void RecountPollinator(Pollinator pollinator, TRuleArgument ruleArgument);
    }
}
