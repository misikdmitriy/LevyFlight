using System;
using LevyFlight.Domain.Algorithms;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Factories
{
    internal abstract class AlgorithmCreator<TAlgorithmSettings>
        where TAlgorithmSettings : AlgorithmSettings
    {
        public abstract AlgorithmPerformer Create(Func<double[], double> functionStrategy, int variablesCount,
            TAlgorithmSettings algorithmSettings);
    }
}
