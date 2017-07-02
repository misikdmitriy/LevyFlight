namespace LevyFlight.Entities
{
    public class AlgorithmSettings
    {
        public bool IsMin { get; set; }
        public int PollinatorsCount { get; set; }
        public int GroupsCount { get; set; }
        public int MaxGeneration { get; set; }
        public double P { get; set; }

        public static AlgorithmSettings Default => new AlgorithmSettings
        {
            GroupsCount = 20,
            IsMin = true,
            MaxGeneration = 1000,
            P = 0.85,
            PollinatorsCount = 10,
        };
    }
}
