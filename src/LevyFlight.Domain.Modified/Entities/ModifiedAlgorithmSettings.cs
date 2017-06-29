using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Entities
{
    public class ModifiedAlgorithmSettings : AlgorithmSettings
    {
        public double PReset { get; set; }

        public new static ModifiedAlgorithmSettings Default => new ModifiedAlgorithmSettings
        {
            GroupsCount = 20,
            IsMin = true,
            MaxGeneration = 1000,
            P = 0.85,
            PollinatorsCount = 10,
            VariablesCount = 30,
            PReset = 0.01
        };
    }
}
