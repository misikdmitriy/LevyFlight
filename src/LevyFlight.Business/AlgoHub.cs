using System;
using System.Threading.Tasks;
using LevyFlight.Entities;
using LevyFlight.Extensions;

namespace LevyFlight.Business
{
    using AlgorithmCreator = Domain.Modified.Factories.AlgorithmCreator;

    public class AlgoHub
    {
        public async Task<double> FindMinimumAsync(Func<double[], double> function, int variablesCount)
        {
            var algorithmPerformer = new AlgorithmCreator().Create(function, variablesCount);
            var resultPollinator = await algorithmPerformer.PolinateAsync();

            return resultPollinator.CountFunction(function, Solution.Current);
        }
    }
}
