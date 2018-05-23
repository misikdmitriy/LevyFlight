using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Entities;
using LevyFlight.Logic.Visitors;

namespace LevyFlight.Domain.Modified.Rules
{
    internal sealed class LocalPollinationRule : IRule<LocalPollinationRuleArgument>
    {
        public Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, LocalPollinationRuleArgument ruleArgument)
        {
            var randomPollinator = ruleArgument.RandomPollinator;

            return Task.Run(() =>
            {
                var ruleVisitor = new TwoPollinatorsVisitor((first, second) =>
                {
                    var values = new double[pollinator.Size];

                    for (var i = 0; i < pollinator.Size; i++)
                    {
                        var rand = RandomGenerator.Random.NextDouble();
                        values[i] = first[i] + rand * (second[i] - first[i]);
                    }

                    return values;
                });

                return new Pollinator(ruleVisitor.Visit(pollinator, randomPollinator));
            });
        }
    }
}
