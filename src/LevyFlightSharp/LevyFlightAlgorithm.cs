using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace LevyFlightSharp
{
    public class LevyFlightAlgorithm
    {
        private Settings Settings { get; }
        private FlowersGroup[] Groups { get; }

        public LevyFlightAlgorithm()
        {
            Settings = new Settings();
            ConfigurationService.Configuration
                .GetSection("Settings:AlgorithmSettings")
                .Bind(Settings);

            Groups = new FlowersGroup[Settings.GroupsCount];

            for (var i = 0; i < Settings.GroupsCount; i++)
            {
                Groups[i] = new FlowersGroup(Settings.FlowersCount, Settings.VariablesCount, RastriginFunction);
            }
        }

        public virtual Flower Polinate()
        {
            var t = 0;

            while (t < Settings.MaxGeneration)
            {
                PolinateOnce(Groups);

                ++t;
            }

            return Groups.FindGlobalBest(Settings.IsMin);
        }

        protected virtual void PolinateOnce(FlowersGroup[] groups)
        {
            foreach (var group in groups)
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

        protected virtual FlowersGroup[] CreateLocalBestGroup()
        {
            var localBestFlowers = Groups
                .Select(g => g.BestSolution)
                .ToArray();

            return new[] { new FlowersGroup(localBestFlowers) };
        }

        private static double GriewankFunction(double[] flowers)
        {
            var sum1 = 0.0;
            var sum2 = 1.0;

            var num = 1;
            foreach (var x in flowers)
            {
                sum1 += x * x / 4000.0;
                sum2 *= Math.Cos(x / Math.Sqrt(num));
                ++num;
            }

            return sum1 - sum2 + 1.0;
        }

        private static double RastriginFunction(double[] flowers)
        {
            var a = 10.0;
            var an = flowers.Length * a;

            var sum1 = flowers.Sum(x => x * x - a * Math.Cos(2 * Math.PI * x));

            return an + sum1;
        }
    }
}
