using System.Collections;
using System.Collections.Generic;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Facade;

namespace LevyFlightSharp.Services
{
    public class FlowersGroup : IEnumerable<Flower>
    {
        private Flower[] Flowers { get; }

        public FlowersGroup(Flower[] flowers)
        {
            Flowers = flowers;
        }

        public FlowersGroup(int sizeOfGroup, int sizeOfFlower,
            FunctionFacade functionFacade)
        {
            Flowers = new Flower[sizeOfGroup];
            for (var i = 0; i < sizeOfGroup; i++)
            {
                Flowers[i] = new Flower(sizeOfFlower, functionFacade);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Flower> GetEnumerator()
        {
            var iterator = Flowers.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return (Flower)iterator.Current;
            }
        }
    }
}
