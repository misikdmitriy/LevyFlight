using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Entities;
using LevyFlightSharp.Services;

namespace LevyFlightSharp.Mediator
{
    public class BestSolutionRequest : IRequest<Flower>
    {
        public FlowersGroup[] FlowerGroups { get; }
        public bool IsMin { get; }

        public BestSolutionRequest(FlowersGroup[] flowerGroups, bool isMin)
        {
            FlowerGroups = flowerGroups;
            IsMin = isMin;
        }
    }

    public class BestSolutionRequestHandler : IRequestHandler<BestSolutionRequest, Flower>
    {
        private readonly FlowerAnalyzer _analyzer;

        public BestSolutionRequestHandler()
        {
            _analyzer = new FlowerAnalyzer();
        }

        public Flower Handle(BestSolutionRequest request)
        {
            return _analyzer.GetBestSolution(request.FlowerGroups, request.IsMin);
        }
    }
}
