using LevyFlight.Domain.Factories;
using LevyFlight.Domain.Modified.FunctionStrategies;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Domain.Rules;

namespace LevyFlight.Domain.Modified.Factories
{
    internal sealed class RuleFactory : IRuleFactory<GlobalPollinationRuleArgument, LocalPollinationRuleArgument>
    {
        public Rule<GlobalPollinationRuleArgument> CreateGlobalPollinationRule()
        {
            return new GlobalPollinationRule(new DistanceFunctionStrategy(), 
                new LambdaFunctionStrategy(), 
                new MantegnaFunctionStrategy());
        }

        public Rule<LocalPollinationRuleArgument> CreateLocalPollinationRule()
        {
            return new LocalPollinationRule();
        }
    }
}
