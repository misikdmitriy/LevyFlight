using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Entities
{
    internal class ModifiedAlgorithmSettings : AlgorithmSettings
    {
        public double PReset { get; }

        public ModifiedAlgorithmSettings(int groupsCount, bool isMin, int maxGeneration, double p, 
            int pollinatorsCount, double pReset) 
            : base(groupsCount, isMin, maxGeneration, p, pollinatorsCount)
        {
            PReset = pReset;
        }
    }
}
