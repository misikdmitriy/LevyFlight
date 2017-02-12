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

        public override Flower Polinate()
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

        protected override void PostOperationAction(Flower flower)
        {
            try
            {
                base.PostOperationAction(flower);
            }
            catch (ArithmeticException e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        protected override void GoFirstBranch(FlowersGroup group, Flower flower)
        {
            _logger.LogTrace("Flower " + group.IndexOf(flower) + " goes first branch");
            base.GoFirstBranch(group, flower);
            _logger.LogTrace("New values = " + flower.ToString(Solution.NewSolution));
            _logger.LogTrace("Func = " + flower.CountFunction(Solution.NewSolution));
        }

        protected override void GoSecondBranch(FlowersGroup group, Flower flower)
        {
            _logger.LogTrace("Flower " + group.IndexOf(flower) + " goes second branch");
            base.GoSecondBranch(group, flower);
            _logger.LogTrace("New values = " + flower.ToString(Solution.NewSolution));
            _logger.LogTrace("Func = " + flower.CountFunction(Solution.NewSolution));
        }

        protected void FindBestSolution(FlowersGroup group)
        {
            var result = Mediator.Send(new BestSolutionRequest(new[] { group }, Settings.IsMin));

            _logger.LogDebug("Group. Local minimum. Func = "
                + result.CountFunction(Solution.Current));

            _logger.LogDebug("Values = " + result.ToString(Solution.Current));
        }
    }
}
