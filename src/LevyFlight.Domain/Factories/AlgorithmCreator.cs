using LevyFlight.Domain.Algorithms;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.FunctionStrategies;

namespace LevyFlight.Domain.Factories
{
    internal abstract class AlgorithmCreator<TAlgorithm, TGpArgument, TLpArgument> where TGpArgument : RuleArgument
        where TLpArgument : RuleArgument
        where TAlgorithm : AlgorithmPerformer<TGpArgument, TLpArgument>
    {
        public abstract TAlgorithm Create(IFunctionStrategy functionStrategy, int variablesCount);
    }
}
