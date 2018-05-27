using System;
using LevyFlight.Domain.Modified.Entities;
using LevyFlight.Domain.Modified.Rules;
using LevyFlight.Logic.Factories;
using NLog;
using AlgorithmPerformer = LevyFlight.Domain.Algorithms.AlgorithmPerformer;
using Logger = LevyFlight.Logging.Logger;

namespace LevyFlight.Domain.Modified.Factories
{
    internal sealed class AlgorithmCreator : Domain.Factories.AlgorithmCreator<ModifiedAlgorithmSettings>
    {
        public override AlgorithmPerformer Create(Func<double[], double> functionStrategy, int variablesCount,
            ModifiedAlgorithmSettings algorithmSettings)
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
