using System;
using LevyFlight.Domain.Algorithms;

namespace LevyFlight.Domain.Factories
{
    internal abstract class AlgorithmCreator
    {
        public abstract AlgorithmPerformer Create(Func<double[], double> functionStrategy, int variablesCount);
    }
}
