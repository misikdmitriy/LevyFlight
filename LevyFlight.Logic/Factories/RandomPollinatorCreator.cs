using System.Linq;
using LevyFlight.Common.Misc;
using LevyFlight.Entities;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Logic.Factories
{
    public class RandomPollinatorCreator : IPollinatorCreator
    {
        public Pollinator Create(int variablesCount)
        {
            return new Pollinator(Enumerable.Repeat(RandomGenerator.Random.NextDouble(), variablesCount));
        }
    }
}
