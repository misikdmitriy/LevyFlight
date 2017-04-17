using System.Linq;
using System.Threading.Tasks;

using Autofac;

using LevyFlightSharp.DependencyInjection;
using LevyFlightSharp.Entities;
using LevyFlightSharp.Facade;
using LevyFlightSharp.MediatorRequests;
using LevyFlightSharp.Services;

using MediatR;

using Microsoft.Extensions.Configuration;

namespace LevyFlightSharp.Algorithms
{
    public class LevyFlightAlgorithm
    {
        protected PollinatorsGroup[] Groups { get; }
        protected Settings Settings { get; }
        protected IMediator Mediator { get; } = DependencyRegistration.Container.Resolve<IMediator>();

        public LevyFlightAlgorithm(FunctionFacade functionFacade)
        {
            Settings = new Settings();
            ConfigurationService.Configuration
                .GetSection("AlgorithmSettings")
                .Bind(Settings);

            Groups = new PollinatorsGroup[Settings.GroupsCount];

            for (var i = 0; i < Settings.GroupsCount; i++)
            {
                Groups[i] = new PollinatorsGroup(Settings.PollinatorsCount, Settings.VariablesCount,
                    functionFacade);
            }
        }

        public virtual async Task<Pollinator> PolinateAsync()
        {
            var t = 0;

            while (t < Settings.MaxGeneration)
            {
                await PolinateOnceAsync();

                ++t;
            }

            return await Mediator.Send(new BestSolutionRequest(Groups, Settings.IsMin));
        }

        protected virtual async Task PolinateOnceAsync()
        {
            foreach (var group in Groups)
            {
                foreach (var pollinator in @group)
                {
                    if (RandomGenerator.Random.NextDouble() < Settings.P)
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
            pollinator.TryExchange(Settings.IsMin);
            pollinator.Check();
        }

        protected virtual async Task GoFirstBranchAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            var bestSolutionTask = Mediator.Send(new BestSolutionRequest(new[] { group }, Settings.IsMin));
            var worstSolutionTask = Mediator.Send(new BestSolutionRequest(new[] { group }, !Settings.IsMin));
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
