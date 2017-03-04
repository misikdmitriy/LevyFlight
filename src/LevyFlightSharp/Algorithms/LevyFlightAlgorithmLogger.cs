using System;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Extensions;
using LevyFlightSharp.Facade;
using LevyFlightSharp.Mediator;
using LevyFlightSharp.Services;

using Microsoft.Extensions.Logging;

namespace LevyFlightSharp.Algorithms
{
    public class LevyFlightAlgorithmLogger : LevyFlightAlgorithm
    {
        private readonly ILogger _logger;
        private int _step;

        public LevyFlightAlgorithmLogger(FunctionFacade functionFacade)
            : base(functionFacade)
        {
            _logger = ConfigurationService
                .LoggerFactory
                .CreateLogger(GetType().FullName);
        }

        public override Pollinator Polinate()
        {
            _step = 0;
            var result = base.Polinate();

            _logger.LogInformation("Best solution. Func = " + result.CountFunction(Solution.Current));
            _logger.LogInformation("Values = " + result.ToString(Solution.Current));

            return result;
        }

        protected override void PolinateOnce()
        {
            _logger.LogDebug("Start new step " + _step);

            base.PolinateOnce();

            foreach (var group in Groups)
            {
                FindBestSolution(group);
            }
            ++_step;
        }

        protected override void PostOperationAction(Pollinator pollinator)
        {
            try
            {
                base.PostOperationAction(pollinator);
            }
            catch (ArithmeticException e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        protected override void GoFirstBranch(PollinatorsGroup group, Pollinator pollinator)
        {
            _logger.LogTrace("Pollinator " + group.IndexOf(pollinator) + " goes first branch");
            base.GoFirstBranch(group, pollinator);
            _logger.LogTrace("New values = " + pollinator.ToString(Solution.NewSolution));
            _logger.LogTrace("Func = " + pollinator.CountFunction(Solution.NewSolution));
        }

        protected override void GoSecondBranch(PollinatorsGroup group, Pollinator pollinator)
        {
            _logger.LogTrace("Pollinator " + group.IndexOf(pollinator) + " goes second branch");
            base.GoSecondBranch(group, pollinator);
            _logger.LogTrace("New values = " + pollinator.ToString(Solution.NewSolution));
            _logger.LogTrace("Func = " + pollinator.CountFunction(Solution.NewSolution));
        }

        protected void FindBestSolution(PollinatorsGroup group)
        {
            var result = Mediator.Send(new BestSolutionRequest(new[] { group }, Settings.IsMin));

            _logger.LogDebug("Group. Local minimum. Func = "
                + result.CountFunction(Solution.Current));

            _logger.LogDebug("Values = " + result.ToString(Solution.Current));
        }
    }
}
