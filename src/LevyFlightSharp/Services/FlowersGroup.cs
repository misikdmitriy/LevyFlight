using LevyFlightSharp.Entities;
using LevyFlightSharp.Facade;

namespace LevyFlightSharp.Services
{
    public class FlowersGroup
    {
        public Flower[] Flowers { get; }
        public Flower BestSolution { get; private set; }

        public FlowersGroup(Flower[] flowers)
        {
            Flowers = flowers;

            BestSolution = Flowers[0];
        }

        public FlowersGroup(int sizeOfGroup, int sizeOfFlower,
            FunctionFacade functionFacade)
        {
            Flowers = new Flower[sizeOfGroup];
            for (var i = 0; i < sizeOfGroup; i++)
            {
                Flowers[i] = new Flower(sizeOfFlower, functionFacade);
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
    }
}
