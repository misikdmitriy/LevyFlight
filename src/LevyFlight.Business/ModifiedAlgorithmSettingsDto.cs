using LevyFlight.Domain.Entities;

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

        internal AlgorithmSettings ToModifiedAlgorithmSettings()
        {
            return new AlgorithmSettings(GroupsCount, IsMin, MaxGeneration, P, PollinatorsCount, PReset);
        }
    }
}
