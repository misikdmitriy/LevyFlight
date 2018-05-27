using LevyFlight.Domain.Modified.Entities;

namespace LevyFlight.Business
{
    public class ModifiedAlgorithmSettingsDto
    {
        public bool IsMin { get; }
        public int PollinatorsCount { get; }
        public int GroupsCount { get; }
        public int MaxGeneration { get; }
        public double P { get; }
        public double PReset { get; }

        public ModifiedAlgorithmSettingsDto(int groupsCount, bool isMin, int maxGeneration, double p, 
            int pollinatorsCount, double pReset)
        {
            GroupsCount = groupsCount;
            IsMin = isMin;
            MaxGeneration = maxGeneration;
            P = p;
            PollinatorsCount = pollinatorsCount;
            PReset = pReset;
        }

        internal ModifiedAlgorithmSettings ToModifiedAlgorithmSettings()
        {
            return new ModifiedAlgorithmSettings(GroupsCount, IsMin, MaxGeneration, P, PollinatorsCount, PReset);
        }
    }
}
