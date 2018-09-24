using LevyFlight.Common.Misc;
using LevyFlight.Entities;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Logic.Factories
{
	public class RandomPollinatorCreator : IPollinatorCreator
	{
		public Pollinator Create(int variablesCount)
		{
			var random = RandomGenerator.Random;
			var values = new double[variablesCount];

			for (var i = 0; i < variablesCount; i++)
			{
				values[i] = random.NextDouble();
			}

			return new Pollinator(values);
		}
	}
}
