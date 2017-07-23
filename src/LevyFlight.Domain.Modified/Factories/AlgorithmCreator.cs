using System;
using System.Collections.Generic;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Entities;
using AlgorithmPerformer = LevyFlight.Domain.Algorithms.AlgorithmPerformer;

namespace LevyFlight.Domain.Modified.Factories
{
    internal sealed class AlgorithmCreator : Domain.Factories.AlgorithmCreator
    {
        public override AlgorithmPerformer Create(Func<double[], double> functionStrategy, int variablesCount)
        {
            var settings = ModifiedAlgorithmSettings.Default;

            var groups = new List<PollinatorsGroup>();

            for (var i = 0; i < settings.GroupsCount; i++)
            {
                groups.Add(new PollinatorsGroup(settings.PollinatorsCount, variablesCount));
            }

            return new Algorithms.AlgorithmPerformer(ModifiedAlgorithmSettings.Default, groups.ToArray(), functionStrategy,
                new GlobalPollinationRule(),
                new LocalPollinationRule());
        }
    }
}
