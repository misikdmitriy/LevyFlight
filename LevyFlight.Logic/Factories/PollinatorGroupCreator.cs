using System.Linq;
using LevyFlight.Entities;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Logic.Factories
{
    public class PollinatorGroupCreator : IPollinatorGroupCreator
    {
        private readonly IPollinatorCreator _pollinatorCreator;

        public PollinatorGroupCreator(IPollinatorCreator pollinatorCreator)
        {
            _pollinatorCreator = pollinatorCreator;
        }
        
        public PollinatorsGroup Create(int groupSize, int variablesCount)
        {
            var pollinators = new Pollinator[groupSize];
            for (var i = 0; i < groupSize; i++)
            {
                pollinators[i] = _pollinatorCreator.Create(variablesCount);
            }

            return new PollinatorsGroup(pollinators);
        }
    }
}
