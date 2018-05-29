using System;
using System.Threading.Tasks;
using LevyFlight.Domain.Factories;
using LevyFlight.Extensions;

namespace LevyFlight.Business
{
    public class AlgorithmFacade
    {
        public async Task<double> FindExtremeAsync(Func<double[], double> function, int variablesCount,
            ModifiedAlgorithmSettingsDto algorithmSettings)
        {
            var algorithmPerformer = new AlgorithmCreator().Create(function, variablesCount, 
                algorithmSettings.ToModifiedAlgorithmSettings());
            var resultPollinator = await algorithmPerformer.PolinateAsync();

            return resultPollinator.CountFunction(function);
        }
    }
}
