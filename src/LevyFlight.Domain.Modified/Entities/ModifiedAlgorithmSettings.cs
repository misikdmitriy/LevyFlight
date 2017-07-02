using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Entities
{
    internal class ModifiedAlgorithmSettings : AlgorithmSettings
    {
        public double PReset { get; set; }

        public new static ModifiedAlgorithmSettings Default => new ModifiedAlgorithmSettings
        {
            GroupsCount = 20,
            IsMin = true,
            MaxGeneration = 1000,
            P = 0.85,
            PollinatorsCount = 10,
            PReset = 0.01
        };
    }
}
