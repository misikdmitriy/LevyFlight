using System.Collections;
using System.Collections.Generic;

using LevyFlightSharp.Facade;

namespace LevyFlightSharp.Entities
{
    public class PollinatorsGroup : IEnumerable<Pollinator>
    {
        private Pollinator[] Pollinators { get; }

        public PollinatorsGroup(Pollinator[] pollinators)
        {
            Pollinators = pollinators;
        }

        public PollinatorsGroup(int sizeOfGroup, int variablesCount,
            FunctionFacade functionFacade)
        {
            Pollinators = new Pollinator[sizeOfGroup];
            for (var i = 0; i < sizeOfGroup; i++)
            {
                Pollinators[i] = new Pollinator(variablesCount, functionFacade);
            }
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
