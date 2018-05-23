using LevyFlight.Entities;

namespace LevyFlight.Logic.Factories.Contracts
{
    public interface IPollinatorCreator
    {
        Pollinator Create(int variablesCount);
    }
}
