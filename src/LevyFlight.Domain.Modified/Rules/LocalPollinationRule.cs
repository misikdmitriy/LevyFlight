using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Rules
{
    internal sealed class LocalPollinationRule : IRule<LocalPollinationRuleArgument>
    {
        public Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, LocalPollinationRuleArgument ruleArgument)
        {
            var randomPollinator = ruleArgument.RandomPollinator;

            return Task.Run(() =>
            {
                var rand = RandomGenerator.Random.NextDouble();

                return pollinator + rand * randomPollinator - pollinator;
            });
        }
    }
}
