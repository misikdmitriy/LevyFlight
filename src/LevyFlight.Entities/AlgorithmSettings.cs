namespace LevyFlight.Entities
{
    public class AlgorithmSettings
    {
        public bool IsMin { get; }
        public int PollinatorsCount { get; }
        public int GroupsCount { get; }
        public int MaxGeneration { get;  }
        public double P { get; }

        public AlgorithmSettings(int groupsCount, bool isMin, int maxGeneration, double p, int pollinatorsCount)
        {
            GroupsCount = groupsCount;
            IsMin = isMin;
            MaxGeneration = maxGeneration;
            P = p;
            PollinatorsCount = pollinatorsCount;
        }
    }
}
