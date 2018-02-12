using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Entities;

namespace LevyFlight.Domain.Modified.Rules
{
    internal sealed class LocalPollinationRule : IRule<LocalPollinationRuleArgument>
    {
        public async Task ApplyRuleAsync(Pollinator pollinator, LocalPollinationRuleArgument ruleArgument)
        {
            var randomPollinator = ruleArgument.RandomPollinator;

            await Task.Run(() =>
            {
                pollinator.NewSolution = new double[pollinator.Size];

                for (var i = 0; i < pollinator.Size; i++)
                {
                    var rand = RandomGenerator.Random.NextDouble();

                    pollinator.NewSolution[i] = pollinator.CurrentSolution[i] + rand * (randomPollinator.CurrentSolution[i] -
                                                                                        pollinator.CurrentSolution[i]);
                }
            });
        }
    }
}
