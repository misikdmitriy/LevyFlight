using System.Collections.Generic;
using LevyFlight.Domain.Modified.Algorithms;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Entities;
using LevyFlight.FunctionStrategies;

namespace LevyFlight.Domain.Modified.Factories
{
    internal sealed class AlgorithmCreator 
        : Domain.Factories.AlgorithmCreator<AlgorithmPerformer, 
            GlobalPollinationRuleArgument, LocalPollinationRuleArgument>
    {
        public override AlgorithmPerformer Create(
            IFunctionStrategy functionStrategy, int variablesCount)
        {
            var factory = new RuleFactory();

            var settings = ModifiedAlgorithmSettings.Default;

            var groups = new List<PollinatorsGroup>();

            for (var i = 0; i < settings.GroupsCount; i++)
            {
                groups.Add(new PollinatorsGroup(settings.PollinatorsCount, variablesCount));
            }

            return new AlgorithmPerformer(ModifiedAlgorithmSettings.Default, groups.ToArray(), functionStrategy, 
                factory.CreateGlobalPollinationRule() as GlobalPollinationRule, 
                factory.CreateLocalPollinationRule() as LocalPollinationRule);
        }
    }
}
