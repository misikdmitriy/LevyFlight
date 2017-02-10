using System;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Extensions;
using LevyFlightSharp.Services;
using LevyFlightSharp.Strategies;

using Microsoft.Extensions.Logging;

namespace LevyFlightSharp.Algorithms
{
    public class AlgorithmProxy : LevyFlightAlgorithm
    {
        private readonly ILogger _logger;
        private int _step;

        public AlgorithmProxy(IFunctionStrategy<double, double[]> mainFunctionStrategy, 
            IFunctionStrategy<double, double> mantegnaFunctionStrategy)
            : base(mainFunctionStrategy, mantegnaFunctionStrategy)
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

        protected override bool TryRefindBestSolution(FlowersGroup group)
        {
            var result = base.TryRefindBestSolution(group);

            if (result)
            {
                _logger.LogDebug("Group. New local minimum. Func = " 
                    + group.BestSolution.CountFunction(Solution.Current));

                _logger.LogDebug("Values = " + group.BestSolution.ToString(Solution.Current));
            }

            return result;
        }

        protected override FlowersGroup[] CreateLocalBestGroup()
        {
            _logger.LogTrace("Create group of local best's");
            _step = 0;
            return base.CreateLocalBestGroup();
        }
    }
}
