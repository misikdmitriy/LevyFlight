using System.Linq;
using System.Threading.Tasks;

using LevyFlight.Entities;
using LevyFlight.Facade;
using LevyFlight.MediatorRequests;
using LevyFlight.Services;

using MediatR;

namespace LevyFlight.Algorithms
{
    public class LevyFlightAlgorithm
    {
        protected PollinatorsGroup[] Groups { get; }
        protected AlgorithmSettings AlgorithmSettings { get; }
        protected IMediator Mediator { get; }

        public LevyFlightAlgorithm(FunctionFacade functionFacade, AlgorithmSettings algorithmSettings,
            IMediator mediator)
        {
            AlgorithmSettings = algorithmSettings;
            Mediator = mediator;

            Groups = new PollinatorsGroup[AlgorithmSettings.GroupsCount];

            for (var i = 0; i < AlgorithmSettings.GroupsCount; i++)
            {
                Groups[i] = new PollinatorsGroup(AlgorithmSettings.PollinatorsCount, AlgorithmSettings.VariablesCount,
                    functionFacade);
            }
        }

        public virtual async Task<Pollinator> PolinateAsync()
        {
            var t = 0;

            while (t < AlgorithmSettings.MaxGeneration)
            {
                await PolinateOnceAsync();

                ++t;
            }

            return await Mediator.Send(new BestSolutionRequest(Groups, AlgorithmSettings.IsMin));
        }

        protected virtual async Task PolinateOnceAsync()
        {
            foreach (var group in Groups)
            {
                foreach (var pollinator in @group)
                {
                    if (RandomGenerator.Random.NextDouble() < AlgorithmSettings.P)
                    {
                        await GoFirstBranchAsync(@group, pollinator);
                    }
                    else
                    {
                        GoSecondBranch(@group, pollinator);
                    }

                    PostOperationAction(pollinator);
                }
            }
        }

        protected virtual void PostOperationAction(Pollinator pollinator)
        {
            pollinator.TryExchange(AlgorithmSettings.IsMin);
            pollinator.Check();
        }

        protected virtual async Task GoFirstBranchAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var bestSolutionTask = Mediator.Send(new BestSolutionRequest(new[] { group }, AlgorithmSettings.IsMin));
            var worstSolutionTask = Mediator.Send(new BestSolutionRequest(new[] { group }, !AlgorithmSettings.IsMin));
            await Task.WhenAll(bestSolutionTask, worstSolutionTask);

            pollinator.RecountByFirstBranch(bestSolutionTask.Result, worstSolutionTask.Result);
        }

        protected virtual void GoSecondBranch(PollinatorsGroup group, Pollinator pollinator)
        {
            var i = RandomGenerator.Random.Next() % group.Count();
            pollinator.RecountBySecondBranch(group.ElementAt(i));
        }
    }
}
