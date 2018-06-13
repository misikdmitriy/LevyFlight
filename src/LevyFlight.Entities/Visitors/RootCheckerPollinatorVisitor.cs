using LevyFlight.Entities.Contracts;

namespace LevyFlight.Entities.Visitors
{
    public class RootCheckerPollinatorVisitor : IPollinatorVisitor<bool>
    {
        public bool Visit(Pollinator pollinator)
        {
            foreach (var element in pollinator)
            {
                if (double.IsNaN(element))
                {
                    return false;
                }

                if (double.IsInfinity(element))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
