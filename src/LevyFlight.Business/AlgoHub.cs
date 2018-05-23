using System;
using System.Threading.Tasks;
using LevyFlight.Extensions;

namespace LevyFlight.Business
{
    using AlgorithmCreator = Domain.Modified.Factories.AlgorithmCreator;

    public class AlgoHub
    {
        public async Task<double> FindExtremeAsync(Func<double[], double> function, int variablesCount)
        {
            var algorithmPerformer = new AlgorithmCreator().Create(function, variablesCount);
            var resultPollinator = await algorithmPerformer.PolinateAsync();

            return resultPollinator.CountFunction(function);
        }
    }
}
