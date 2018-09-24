using LevyFlight.Common.Check;

namespace LevyFlight.Domain.Entities
{
	public class AlgorithmSettings
	{
		public static readonly AlgorithmSettings DefaultMin = new AlgorithmSettings(5, true, 50, 0.85, 5, 0.01);
		public static readonly AlgorithmSettings DefaultMax = new AlgorithmSettings(5, false, 50, 0.85, 5, 0.01);

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
