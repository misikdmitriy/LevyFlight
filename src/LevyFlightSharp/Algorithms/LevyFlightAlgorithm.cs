using System.Linq;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Facade;
using LevyFlightSharp.Mediator;
using LevyFlightSharp.Services;

using Microsoft.Extensions.Configuration;

namespace LevyFlightSharp.Algorithms
{
    public class LevyFlightAlgorithm
    {
        protected PollinatorsGroup[] Groups { get; }
        protected Settings Settings { get; }
        protected Mediator.Mediator Mediator { get; } = LevyFlightSharp.Mediator.Mediator.Instance;

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

        public virtual Pollinator Polinate()
        {
            var t = 0;

            while (t < Settings.MaxGeneration)
            {
                PolinateOnce();

                ++t;
            }

            return Mediator.Send(new BestSolutionRequest(Groups, Settings.IsMin));
        }

        protected virtual void PolinateOnce()
        {
            foreach (var group in Groups)
            {
                foreach (var pollinator in @group)
                {
                    if (RandomGenerator.Random.NextDouble() < Settings.P)
                    {
                        GoFirstBranch(@group, pollinator);
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

        protected virtual void GoFirstBranch(PollinatorsGroup group, Pollinator pollinator)
        {
            var bestSolution = Mediator.Send(new BestSolutionRequest(new[] { group }, Settings.IsMin));
            var worstSolution = Mediator.Send(new BestSolutionRequest(new[] { group }, !Settings.IsMin));
            pollinator.RecountByFirstBranch(bestSolution, worstSolution);
        }

        protected virtual void GoSecondBranch(PollinatorsGroup group, Pollinator pollinator)
        {
            var i = RandomGenerator.Random.Next() % group.Count();
            pollinator.RecountBySecondBranch(group.ElementAt(i));
        }
    }
}
