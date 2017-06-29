using System.Runtime.CompilerServices;
using LevyFlight.Domain.Algorithms;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Domain.Rules;
using LevyFlight.Entities;
using LevyFlight.Strategies;

[assembly: InternalsVisibleTo("LevyFlight.Domain.Modified")]

namespace LevyFlight.Domain.Factories
{
    public abstract class AlgorithmCreator<TGpArgument, TLpArgument> where TGpArgument : RuleArgument
        where TLpArgument : RuleArgument
    {
        public abstract AlgorithmPerformer<TGpArgument, TLpArgument> Create(IFunctionStrategy functionStrategy);

        internal abstract AlgorithmPerformer<TGpArgument, TLpArgument> Create(AlgorithmSettings algorithmSettings,
            IFunctionStrategy functionStrategy);

        internal abstract AlgorithmPerformer<TGpArgument, TLpArgument> Create(AlgorithmSettings algorithmSettings,
            IFunctionStrategy functionStrategy, Rule<TGpArgument> globalRule, Rule<TLpArgument> localRule);

        internal abstract AlgorithmPerformer<TGpArgument, TLpArgument> Create(AlgorithmSettings algorithmSettings,
            PollinatorsGroup[] groups,
            IFunctionStrategy functionStrategy, Rule<TGpArgument> globalRule, Rule<TLpArgument> localRule);
    }
}
