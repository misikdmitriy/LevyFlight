namespace LevyFlight.Domain.Entities
{
    public class AlgorithmSettings
    {
        public int GroupsCount { get; }
        public bool IsMin { get; }
        public int PollinatorsCount { get; }
        public int MaxGeneration { get; }
        public double P { get; }
        public double PReset { get; }

        public AlgorithmSettings(int groupsCount, bool isMin, int maxGeneration, double p,
            int pollinatorsCount, double pReset)
        {
            GroupsCount = groupsCount;
            IsMin = isMin;
            MaxGeneration = maxGeneration;
            P = p;
            PollinatorsCount = pollinatorsCount;
            PReset = pReset;
        }
    }
}
