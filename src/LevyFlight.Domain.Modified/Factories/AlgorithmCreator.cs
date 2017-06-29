using System.Collections.Generic;
using LevyFlight.Common.Check;
using LevyFlight.Domain.Algorithms;
using LevyFlight.Domain.Modified.Algorithms;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Domain.Rules;
using LevyFlight.Entities;
using LevyFlight.Strategies;

namespace LevyFlight.Domain.Modified.Factories
{
    public class AlgorithmCreator 
        : Domain.Factories.AlgorithmCreator<GlobalPollinationRuleArgument, LocalPollinationRuleArgument>
    {
        public override AlgorithmPerformer<GlobalPollinationRuleArgument, LocalPollinationRuleArgument> Create(
            IFunctionStrategy functionStrategy)
        {
            return Create(ModifiedAlgorithmSettings.Default, functionStrategy);
        }

        internal override AlgorithmPerformer<GlobalPollinationRuleArgument, LocalPollinationRuleArgument> Create(
            AlgorithmSettings algorithmSettings, IFunctionStrategy functionStrategy)
        {
            var globalPollinationRule = new GlobalPollinationRule(new DistanceFunctionStrategy(), new LambdaFunctionStrategy(),
                new MantegnaFunctionStrategy());

            var localPollinationRule = new LocalPollinationRule();

            return Create(algorithmSettings, functionStrategy, globalPollinationRule, localPollinationRule);
        }

        internal override AlgorithmPerformer<GlobalPollinationRuleArgument, LocalPollinationRuleArgument> Create(
            AlgorithmSettings algorithmSettings, IFunctionStrategy functionStrategy, 
            Rule<GlobalPollinationRuleArgument> globalRule,
            Rule<LocalPollinationRuleArgument> localRule)
        {
            var groups = new List<PollinatorsGroup>();

            for (var i = 0; i < algorithmSettings.GroupsCount; i++)
            {
                var group = new PollinatorsGroup(algorithmSettings.PollinatorsCount, algorithmSettings.VariablesCount);
                groups.Add(group);
            }

            return Create(algorithmSettings, groups.ToArray(), functionStrategy, globalRule,
                localRule);
        }

        internal override AlgorithmPerformer<GlobalPollinationRuleArgument, LocalPollinationRuleArgument> Create(
            AlgorithmSettings algorithmSettings, PollinatorsGroup[] groups,
            IFunctionStrategy functionStrategy, Rule<GlobalPollinationRuleArgument> globalRule, 
            Rule<LocalPollinationRuleArgument> localRule)
        {
            var settings = new ModifiedAlgorithmSettings
            {
                IsMin = algorithmSettings.IsMin,
                PReset = ModifiedAlgorithmSettings.Default.PReset,
                MaxGeneration = algorithmSettings.MaxGeneration,
                VariablesCount = algorithmSettings.VariablesCount,
                PollinatorsCount = algorithmSettings.PollinatorsCount,
                P = algorithmSettings.P,
                GroupsCount = algorithmSettings.GroupsCount
            };

            var globalRuleConverted = ExceptionHandler.Convert<GlobalPollinationRule>(globalRule);
            var localRuleConverted = ExceptionHandler.Convert<LocalPollinationRule>(localRule);

            return Create(settings, groups, functionStrategy, globalRuleConverted, localRuleConverted);
        }

        internal AlgorithmPerformer<GlobalPollinationRuleArgument, LocalPollinationRuleArgument> Create(
            ModifiedAlgorithmSettings algorithmSettings, PollinatorsGroup[] groups,
            IFunctionStrategy functionStrategy, GlobalPollinationRule globalRule,
            LocalPollinationRule localRule)
        {
            return new AlgorithmPerformer(algorithmSettings, groups, functionStrategy, globalRule, localRule);
        }
    }
}
