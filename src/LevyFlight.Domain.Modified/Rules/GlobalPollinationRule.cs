using System.Threading.Tasks;
using LevyFlight.Domain.Modified.RuleArguments;
using LevyFlight.Entities;
using LevyFlight.Extensions;

namespace LevyFlight.Domain.Modified.Rules
{
    internal sealed class GlobalPollinationRule : IRule<GlobalPollinationRuleArgument>
    {
        public Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, GlobalPollinationRuleArgument ruleArgument)
        {
            var bestPollinator = ruleArgument.BestPollinator;
            var worstPollinator = ruleArgument.WorstPollinator;

            return Task.Run(() =>
            {
                var values = new double[pollinator.Size];

                for (var i = 0; i < pollinator.Size; i++)
                {
                    var distanceDifference = worstPollinator.CountFunction(FunctionStrategies.FunctionStrategies.DistanceFunction) -
                                             pollinator.CountFunction(FunctionStrategies.FunctionStrategies.DistanceFunction);

                    var lambda = FunctionStrategies.FunctionStrategies.LambdaFunction(distanceDifference);

                    var rand = FunctionStrategies.FunctionStrategies.MantegnaFunction(lambda);

                    var ruleVisitor = new TwoPollinatorsVisitor((first, second) => first[i] + rand * (second[i] - first[i]));

                    values[i] = ruleVisitor.Visit(pollinator, bestPollinator);
                }

                return new Pollinator(values);
            });
        }
    }
}
