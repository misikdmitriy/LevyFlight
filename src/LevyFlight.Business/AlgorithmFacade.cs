using System;
using System.Linq;
using System.Threading.Tasks;
using LevyFlight.Domain.Contracts;
using LevyFlight.Domain.Factories;
using LevyFlight.Extensions;

namespace LevyFlight.Business
{
    public class AlgorithmFacade
    {
        private readonly IAlgorithmPerformer _algorithmPerformer;
        private readonly Func<double[], double> _function;

        public AlgorithmFacade(Func<double[], double> function, int variablesCount,
            ModifiedAlgorithmSettingsDto algorithmSettings)
        {
            _function = function;

            _algorithmPerformer = new AlgorithmCreator().Create(function, variablesCount,
                algorithmSettings.ToModifiedAlgorithmSettings());
        }

        public event OnStepFinished StepFinished
        {
            add => _algorithmPerformer.StepFinished += value;
            remove => _algorithmPerformer.StepFinished -= value;
        }

        public async Task<double> FindExtremeAsync()
        {
            var resultPollinator = await _algorithmPerformer.PolinateAsync();

            return resultPollinator.CountFunction(_function);
        }

        public async Task<double[]> FindRootAsync()
        {
            var resultPollinator = await _algorithmPerformer.PolinateAsync();

            return resultPollinator.ToArray();
        }
    }
}
