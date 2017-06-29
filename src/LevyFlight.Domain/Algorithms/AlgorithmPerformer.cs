using LevyFlight.Common.Misc;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Domain.Rules;
using LevyFlight.Strategies;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Algorithms
{
    public abstract class AlgorithmPerformer<TGpArgument, TLpArgument> where TGpArgument : RuleArgument
        where TLpArgument : RuleArgument
    {
        protected PollinatorsGroup[] Groups { get; }
        protected AlgorithmSettings AlgorithmSettings { get; }
        protected IFunctionStrategy FunctionStrategy { get; }

        protected IRule<TGpArgument> GlobalPollinationRule { get; }
        protected IRule<TLpArgument> LocalPollinationRule { get; }

        protected AlgorithmPerformer(AlgorithmSettings algorithmSettings, PollinatorsGroup[] groups, 
            IFunctionStrategy functionStrategy, IRule<TGpArgument> globalPollinationRule, 
            IRule<TLpArgument> localPollinationRule)
        {
            AlgorithmSettings = algorithmSettings;
            Groups = groups;
            FunctionStrategy = functionStrategy;
            GlobalPollinationRule = globalPollinationRule;
            LocalPollinationRule = localPollinationRule;
        }

        public virtual Pollinator Polinate()
        {
            var t = 0;

            while (t < AlgorithmSettings.MaxGeneration)
            {
                PolinateOnce();

                ++t;
            }

            return Groups.GetBestSolution(FunctionStrategy, AlgorithmSettings.IsMin);
        }

        private void PolinateOnce()
        {
            foreach (var group in Groups)
            {
                foreach (var pollinator in group)
                {
                    if (RandomGenerator.Random.NextDouble() < AlgorithmSettings.P)
                    {
                        GoFirstBranch(group, pollinator);
                    }
                    else
                    {
                        GoSecondBranch(group, pollinator);
                    }

                    PostOperationAction(group, pollinator);
                }
            }
        }

        protected abstract void GoFirstBranch(PollinatorsGroup group, Pollinator pollinator);
        protected abstract void GoSecondBranch(PollinatorsGroup group, Pollinator pollinator);
        protected abstract void PostOperationAction(PollinatorsGroup group, Pollinator pollinator);
    }
}
