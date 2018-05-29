using System;
using System.Threading.Tasks;
using LevyFlight.Common.Misc;
using LevyFlight.Domain.RuleArguments;
using LevyFlight.Entities;
using LevyFlight.Extensions;
using LevyFlight.Logging.Contracts;
using LevyFlight.Logic.Factories.Contracts;

namespace LevyFlight.Domain.Rules
{
    public class ResetPollinationRule : IRule<ResetPollinationRuleArgument>
    {
        private readonly ILogger _logger;
        private readonly Func<double[], double> _functionStrategy;
        private readonly IPollinatorUpdater _pollinatorUpdater;

        private readonly bool _isMin;
        private readonly double _pReset;

        public ResetPollinationRule(IPollinatorUpdater pollinatorUpdater, ILogger logger,
            Func<double[], double> functionStrategy, bool isMin, double pReset)
        {
            _logger = logger;
            _functionStrategy = functionStrategy;
            _isMin = isMin;
            _pReset = pReset;
            _pollinatorUpdater = pollinatorUpdater;
        }

        public Task<Pollinator> ApplyRuleAsync(Pollinator pollinator, ResetPollinationRuleArgument ruleArgument)
        {
            var current = ruleArgument.NewPollinator;
            var group = ruleArgument.Group;

            return Task.Run(() =>
            {
                if (!current.CheckWhetherValuesCorrect())
                {
                    _logger.Error($"Pollinator got NaN or +/- Infinity. Pollinator - {current.ToArrayRepresentation()}");
                    return pollinator;
                }

                Pollinator best;

                var currentSolution = pollinator.CountFunction(_functionStrategy);
                var newSolution = current.CountFunction(_functionStrategy);

                _logger.Trace($"Current solution {currentSolution}");
                _logger.Trace($"New solution {newSolution}");

                if (currentSolution > newSolution && _isMin ||
                    newSolution > currentSolution && !_isMin)
                {
                    group.Replace(pollinator, current);
                    best = current;
                }
                else
                {
                    best = pollinator;
                }

                if (RandomGenerator.Random.NextDouble() < _pReset)
                {
                    _logger.Trace($"Before pollinator reset is {best.ToArrayRepresentation()}");

                    var generated = _pollinatorUpdater.Update(best);
                    group.Replace(best, generated);
                    best = generated;

                    _logger.Trace($"After pollinator reset is {best.ToArrayRepresentation()}");
                }

                return best;
            });
        }
    }
}
