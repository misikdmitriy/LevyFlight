using System.Linq;
using LevyFlight.Common.Misc;
using LevyFlight.Entities;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Logic.Factories
{
	public class PollinatorUpdater : IPollinatorUpdater
	{
		public Pollinator Update(Pollinator pollinator)
		{
			var values = pollinator.ToArray();

			var i = RandomGenerator.Random.Next() % pollinator.Size;
			values[i] = RandomGenerator.Random.NextDouble();

			return new Pollinator(values);
		}
	}
}
