using LevyFlight.Entities.Contracts;

namespace LevyFlight.Entities.Visitors
{
    public class ToStringPollinatorVisitor : IPollinatorVisitor<string>
    {
        public string Visit(Pollinator pollinator)
        {
            return string.Join(";", pollinator);
        }
    }
}
