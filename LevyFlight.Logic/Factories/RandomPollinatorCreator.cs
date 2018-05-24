using System;
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
            var values = new double[variablesCount];
            for (var i = 0; i < variablesCount; i++)
            {
                values[i] = RandomGenerator.Random.NextDouble();
            }

            return new Pollinator(values);
        }
    }
}
