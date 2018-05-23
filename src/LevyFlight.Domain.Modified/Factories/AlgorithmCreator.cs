using System;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Logic.Factories;
using AlgorithmPerformer = LevyFlight.Domain.Algorithms.AlgorithmPerformer;

namespace LevyFlight.Domain.Modified.Factories
{
    internal sealed class AlgorithmCreator : Domain.Factories.AlgorithmCreator
    {
        public override AlgorithmPerformer Create(Func<double[], double> functionStrategy, int variablesCount)
        {
            return new Algorithms.AlgorithmPerformer(ModifiedAlgorithmSettings.Default, variablesCount, functionStrategy,
                new PollinatorGroupCreator(new RandomPollinatorCreator()),
                new PollinatorUpdater(), 
                new GlobalPollinationRule(),
                new LocalPollinationRule());
        }
    }
}
