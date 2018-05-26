using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevyFlight.Common.Check;

namespace LevyFlight.Entities
{
    public class PollinatorsGroup : IEnumerable<Pollinator>
    {
        private readonly Pollinator[] _pollinators;

        public PollinatorsGroup(IEnumerable<Pollinator> pollinators)
        {
            _pollinators = pollinators.ToArray();
        }

        public PollinatorsGroup(int sizeOfGroup, int variablesCount)
        {
            _pollinators = new Pollinator[sizeOfGroup];
            for (var i = 0; i < sizeOfGroup; i++)
            {
                _pollinators[i] = new Pollinator(Enumerable.Repeat(0.0, variablesCount));
            }
        }

        public void Replace(Pollinator target, Pollinator source)
        {
            ExceptionHelper.ThrowExceptionIfNotEqual(target.Size, source.Size);

            _pollinators[Array.IndexOf(_pollinators, target)] = source;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Pollinator> GetEnumerator()
        {
            var enumerator = _pollinators.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return (Pollinator)enumerator.Current;
            }
        }
    }
}
