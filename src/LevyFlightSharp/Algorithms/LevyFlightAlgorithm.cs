using LevyFlightSharp.Entities;
using LevyFlightSharp.Extensions;
using LevyFlightSharp.Facade;
using LevyFlightSharp.Services;

using Microsoft.Extensions.Configuration;

namespace LevyFlightSharp.Algorithms
{
    public class LevyFlightAlgorithm
    {
        private Settings Settings { get; }
        private FlowersGroup[] Groups { get; }

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

            return Groups.FindGlobalBest(Settings.IsMin);
        }

        protected virtual void PolinateOnce()
        {
            foreach (var group in Groups)
            {
                foreach (var flower in @group.Flowers)
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

                TryRefindBestSolution(@group);
            }
        }

        protected virtual void PostOperationAction(Flower flower)
        {
            flower.TryExchange(Settings.IsMin);
            flower.Check();
        }

        protected virtual void GoFirstBranch(FlowersGroup group, Flower flower)
        {
            flower.RecountByFirstBranch(group.BestSolution);
        }

        protected virtual void GoSecondBranch(FlowersGroup group, Flower flower)
        {
            var i = RandomGenerator.Random.Next() % group.Flowers.Length;
            flower.RecountBySecondBranch(group.Flowers[i]);
        }

        protected virtual bool TryRefindBestSolution(FlowersGroup group)
        {
            return group.TryRefindBestSolution(Settings.IsMin);
        }
    }
}
