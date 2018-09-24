using System;
using System.Linq;
using LevyFlight.Entities;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Logic.Factories
{
	public class PollinatorUpdater : IPollinatorUpdater
	{
		public Pollinator Update(Pollinator pollinator)
		{
			var random = new Random((int) DateTime.Now.Ticks);

			var values = pollinator.ToArray();

			var i = random.Next() % pollinator.Size;
			values[i] = random.NextDouble();

			return new Pollinator(values);
		}
	}
}
