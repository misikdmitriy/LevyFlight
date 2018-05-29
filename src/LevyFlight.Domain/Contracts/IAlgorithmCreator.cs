using System;
using LevyFlight.Domain.Entities;

namespace LevyFlight.Domain.Contracts
{
    internal interface IAlgorithmCreator<in TAlgorithmSettings>
        where TAlgorithmSettings : AlgorithmSettings
    {
        IAlgorithmPerformer Create(Func<double[], double> functionStrategy, int variablesCount,
            TAlgorithmSettings algorithmSettings);
    }
}
