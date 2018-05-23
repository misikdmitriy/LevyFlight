using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevyFlight.Common.Check;

namespace LevyFlight.Entities
{
    public class PollinatorsGroup : IEnumerable<Pollinator>
    {
        private Pollinator[] Pollinators { get; }

        public PollinatorsGroup(IEnumerable<Pollinator> pollinators)
        {
            Pollinators = pollinators.ToArray();
        }

        public PollinatorsGroup(int sizeOfGroup, int variablesCount)
        {
            Pollinators = new Pollinator[sizeOfGroup];
            for (var i = 0; i < sizeOfGroup; i++)
            {
                Pollinators[i] = new Pollinator(Enumerable.Repeat(0.0, variablesCount));
            }
        }

        public void Replace(Pollinator target, Pollinator source)
        {
            ExceptionHelper.ThrowExceptionIfNotEqual(target.Size, source.Size);

            Pollinators[Array.IndexOf(Pollinators, target)] = source;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Pollinator> GetEnumerator()
        {
            var iterator = Pollinators.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return (Pollinator)iterator.Current;
            }
        }
    }
}
