namespace LevyFlight.Entities.Contracts
{
    public interface IPollinatorVisitor<out T>
    {
        T Visit(Pollinator pollinator);
    }
}
