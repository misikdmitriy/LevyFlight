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
        protected FlowersGroup[] Groups { get; }
        protected Settings Settings { get; }
        protected Mediator.Mediator Mediator { get; } = LevyFlightSharp.Mediator.Mediator.Instance;

        public LevyFlightAlgorithm(FunctionFacade functionFacade)
        {
            Settings = new Settings();
            ConfigurationService.Configuration
                .GetSection("AlgorithmSettings")
                .Bind(Settings);

            Groups = new FlowersGroup[Settings.GroupsCount];

            for (var i = 0; i < Settings.GroupsCount; i++)
            {
                Groups[i] = new FlowersGroup(Settings.FlowersCount, Settings.VariablesCount,
                    functionFacade);
            }
        }

        public virtual Flower Polinate()
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
                foreach (var flower in @group)
                {
                    if (RandomGenerator.Random.NextDouble() < Settings.P)
                    {
                        GoFirstBranch(@group, flower);
                    }
                    else
                    {
                        GoSecondBranch(@group, flower);
                    }

                    PostOperationAction(flower);
                }
            }
        }

        protected virtual void PostOperationAction(Flower flower)
        {
            flower.TryExchange(Settings.IsMin);
            flower.Check();
        }

        protected virtual void GoFirstBranch(FlowersGroup group, Flower flower)
        {
            var bestSolution = Mediator.Send(new BestSolutionRequest(new[] { group }, Settings.IsMin));
            var worstSolution = Mediator.Send(new BestSolutionRequest(new[] { group }, !Settings.IsMin));
            flower.RecountByFirstBranch(bestSolution, worstSolution);
        }

        protected virtual void GoSecondBranch(FlowersGroup group, Flower flower)
        {
            var i = RandomGenerator.Random.Next() % group.Count();
            flower.RecountBySecondBranch(group.ElementAt(i));
        }
    }
}
