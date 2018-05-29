using System;
using LevyFlight.Domain.Contracts;
using LevyFlight.Domain.Entities;
using LevyFlight.Domain.Rules;
using LevyFlight.Logic.Factories;
using NLog;
using Logger = LevyFlight.Logging.Logger;

namespace LevyFlight.Domain.Factories
{
    internal sealed class AlgorithmCreator : IAlgorithmCreator<AlgorithmSettings>
    {
        public IAlgorithmPerformer Create(Func<double[], double> functionStrategy, int variablesCount,
            AlgorithmSettings algorithmSettings)
        {
            return new Algorithms.AlgorithmPerformer(algorithmSettings, 
                variablesCount, 
                functionStrategy,
                new PollinatorGroupCreator(new RandomPollinatorCreator()), 
                new GlobalPollinationRule(new Logger(LogManager.GetLogger(nameof(GlobalPollinationRule)))),
                new LocalPollinationRule(new Logger(LogManager.GetLogger(nameof(LocalPollinationRule)))),
                new ResetPollinationRule(new PollinatorUpdater(), 
                    new Logger(LogManager.GetLogger(nameof(ResetPollinationRule))),
                    functionStrategy,
                    algorithmSettings.IsMin,
                    algorithmSettings.PReset),
                new Logger(LogManager.GetLogger(nameof(Algorithms.AlgorithmPerformer))));
        }
    }
}
