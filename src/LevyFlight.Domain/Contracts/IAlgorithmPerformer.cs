using System.Threading.Tasks;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Contracts
{
	public delegate void OnStepFinished(object sender, StepFinishedArgs args);

	public interface IAlgorithmPerformer
	{
		Task<Pollinator> PolinateAsync();
		event OnStepFinished StepFinished;
	}

	public class StepFinishedArgs
	{
		public Pollinator Best { get; }
		public int Step { get; }

		public StepFinishedArgs(Pollinator best, int step)
		{
			Best = best;
			Step = step;
		}
	}
}
