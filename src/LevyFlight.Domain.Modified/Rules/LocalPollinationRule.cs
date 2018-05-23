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
                var values = new double[pollinator.Size];

                for (var i = 0; i < pollinator.Size; i++)
                {
                    var rand = RandomGenerator.Random.NextDouble();

                    var ruleVisitor = new TwoPollinatorsVisitor((first, second) => first[i] + rand * (second[i] - first[i]));

                    values[i] = ruleVisitor.Visit(pollinator, randomPollinator);
                }

                return new Pollinator(values);
            });
        }
    }
}
