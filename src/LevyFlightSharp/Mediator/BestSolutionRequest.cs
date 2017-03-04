using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Entities;
using LevyFlightSharp.Services;

namespace LevyFlightSharp.Mediator
{
    public class BestSolutionRequest : IRequest<Pollinator>
    {
        public PollinatorsGroup[] PollinatorGroups { get; }
        public bool IsMin { get; }

        public BestSolutionRequest(PollinatorsGroup[] pollinatorGroups, bool isMin)
        {
            PollinatorGroups = pollinatorGroups;
            IsMin = isMin;
        }
    }

    public class BestSolutionRequestHandler : IRequestHandler<BestSolutionRequest, Pollinator>
    {
        private readonly PollinatorAnalyzer _analyzer;

        public BestSolutionRequestHandler()
        {
            _analyzer = new PollinatorAnalyzer();
        }

        public Pollinator Handle(BestSolutionRequest request)
        {
            return _analyzer.GetBestSolution(request.PollinatorGroups, request.IsMin);
        }
    }
}
