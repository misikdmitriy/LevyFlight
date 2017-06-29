using LevyFlight.Common.Misc;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Domain.Rules;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Rules
{
    public class LocalPollinationRule : IRule<LocalPollinationRuleArgument>
    {
        public void RecountPollinator(Pollinator pollinator, LocalPollinationRuleArgument ruleArgument)
        {
            var randomPollinator = ruleArgument.RandomPollinator;

            pollinator.NewSolution = new double[pollinator.Size];

            for (var i = 0; i < pollinator.Size; i++)
            {
                var rand = RandomGenerator.Random.NextDouble();

                pollinator.NewSolution[i] = pollinator.CurrentSolution[i] + rand * (randomPollinator.CurrentSolution[i] - 
                    pollinator.CurrentSolution[i]);
            }
        }
    }
}
