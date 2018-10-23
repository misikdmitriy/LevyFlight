using LevyFlight.Common.Check;

namespace LevyFlight.Domain.Entities
{
	public class AlgorithmSettings
	{
		public const int DefaultGroupsCount = 2;
		public const int DefaultPollinatorsCount = 25;
		public const int DefaultMaxGeneration = 30;
		public const double DefaultP = 0.91;
		public const double DefaultPReset = 0.01;

		public static readonly AlgorithmSettings DefaultMin = new AlgorithmSettings(DefaultGroupsCount, true, DefaultMaxGeneration, DefaultP, DefaultPollinatorsCount, DefaultPReset);
		public static readonly AlgorithmSettings DefaultMax = new AlgorithmSettings(DefaultGroupsCount, false, DefaultMaxGeneration, DefaultP, DefaultPollinatorsCount, DefaultPReset);

		public int GroupsCount { get; }
		public bool IsMin { get; }
		public int PollinatorsCount { get; }
		public int MaxGeneration { get; }
		public double P { get; }
		public double PReset { get; }

		public AlgorithmSettings(int groupsCount, bool isMin, int maxGeneration, double p,
			int pollinatorsCount, double pReset)
		{
			ThrowIf.NotPositive(groupsCount, nameof(groupsCount));
			ThrowIf.NotPositive(pollinatorsCount, nameof(pollinatorsCount));
			ThrowIf.NotPositive(maxGeneration, nameof(maxGeneration));
			ThrowIf.NotBetweenZeroAndOne(p, nameof(p));
			ThrowIf.NotBetweenZeroAndOne(pReset, nameof(pReset));

			GroupsCount = groupsCount;
			IsMin = isMin;
			MaxGeneration = maxGeneration;
			P = p;
			PollinatorsCount = pollinatorsCount;
			PReset = pReset;
		}
	}
}
