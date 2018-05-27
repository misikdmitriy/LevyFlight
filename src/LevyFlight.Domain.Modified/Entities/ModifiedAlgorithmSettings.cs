using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Entities
{
    internal class ModifiedAlgorithmSettings : AlgorithmSettings
    {
        public double PReset { get; }

        public new static ModifiedAlgorithmSettings Default => new ModifiedAlgorithmSettings(
            AlgorithmSettings.Default.GroupsCount,
            AlgorithmSettings.Default.IsMin,
            AlgorithmSettings.Default.MaxGeneration,
            AlgorithmSettings.Default.P,
            AlgorithmSettings.Default.PollinatorsCount,
            0.01);

        public ModifiedAlgorithmSettings(int groupsCount, bool isMin, int maxGeneration, double p, int pollinatorsCount, 
            double pReset) : base(groupsCount, isMin, maxGeneration, p, pollinatorsCount)
        {
            PReset = pReset;
        }
    }
}
