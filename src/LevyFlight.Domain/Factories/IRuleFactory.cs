using LevyFlight.Domain.RuleArguments;
using LevyFlight.Domain.Rules;

namespace LevyFlight.Domain.Factories
{
    internal interface IRuleFactory<TGpRuleArgument, TLpRuleArgument>
        where TGpRuleArgument : RuleArgument
    {
        Rule<TGpRuleArgument> CreateGlobalPollinationRule();
        Rule<TLpRuleArgument> CreateLocalPollinationRule();
    }
}
