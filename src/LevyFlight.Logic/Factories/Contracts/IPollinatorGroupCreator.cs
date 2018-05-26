using LevyFlight.Entities;

namespace LevyFlight.Logic.Factories.Contracts
{
    public interface IPollinatorGroupCreator
    {
        PollinatorsGroup Create(int groupSize, int variablesCount);
    }
}
