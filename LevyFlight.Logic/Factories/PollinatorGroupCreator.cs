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
            return new PollinatorsGroup(Enumerable.Repeat(_pollinatorCreator.Create(variablesCount), groupSize));
        }
    }
}
