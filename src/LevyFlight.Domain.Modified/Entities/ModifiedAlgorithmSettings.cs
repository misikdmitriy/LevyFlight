using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Entities
{
    internal class ModifiedAlgorithmSettings : AlgorithmSettings
    {
        public double PReset { get; set; }

        public new static ModifiedAlgorithmSettings Default => new ModifiedAlgorithmSettings(20, true, 1000, 0.85, 10,
            0.01);

        public ModifiedAlgorithmSettings(int groupsCount, bool isMin, int maxGeneration, double p, int pollinatorsCount, 
            double pReset) : base(groupsCount, isMin, maxGeneration, p, pollinatorsCount)
        {
            PReset = pReset;
        }
    }
}
