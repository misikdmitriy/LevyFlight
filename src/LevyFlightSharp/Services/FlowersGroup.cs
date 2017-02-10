using System.Collections;
using System.Collections.Generic;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Services
{
    public class FlowersGroup : IEnumerable<Flower>
    {
        public Flower[] Flowers { get; }
        public Flower BestSolution { get; private set; }

        public FlowersGroup(Flower[] flowers)
        {
            Flowers = flowers;

            BestSolution = Flowers[0];
        }

        public FlowersGroup(int sizeOfGroup, int sizeOfFlower,
            IFunctionStrategy<double, double[]> mainFunction, 
            IFunctionStrategy<double, double> mantegnaFunctionStrategy)
        {
            Flowers = new Flower[sizeOfGroup];
            for (var i = 0; i < sizeOfGroup; i++)
            {
                Flowers[i] = new Flower(sizeOfFlower, mainFunction, mantegnaFunctionStrategy);
            }

            BestSolution = Flowers[0];
        }

        public bool TryRefindBestSolution(bool isMin = true)
        {
            var bestFunc = BestSolution.CountFunction(Solution.Current);
            var founded = false;

            foreach (var flower in Flowers)
            {
                var func = flower.CountFunction(Solution.Current);

                if (isMin && bestFunc > func
                    || !isMin && bestFunc < func)
                {
                    founded = true;

                    bestFunc = func;
                    BestSolution = flower;
                }
            }

            return founded;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Flowers.GetEnumerator();
        }

        public IEnumerator<Flower> GetEnumerator()
        {
            return (IEnumerator<Flower>)Flowers.GetEnumerator();
        }
    }
}
