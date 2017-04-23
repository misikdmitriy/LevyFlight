using System;
using System.Threading.Tasks;

using LevyFlightSharp.Entities;
using LevyFlightSharp.Extensions;
using LevyFlightSharp.Facade;
using LevyFlightSharp.MediatorRequests;
using LevyFlightSharp.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace LevyFlightSharp.Algorithms
{
    public class LevyFlightAlgorithmLogger : LevyFlightAlgorithm
    {
        private readonly ILogger _logger;
        private int _step;

        public LevyFlightAlgorithmLogger(FunctionFacade functionFacade, AlgorithmSettings algorithmSettings,
            IMediator mediator)
            : base(functionFacade, algorithmSettings, mediator)
        {
            _logger = ConfigurationService
                .LoggerFactory
                .CreateLogger(GetType().FullName);
        }

        public override async Task<Pollinator> PolinateAsync()
        {
            _step = 0;
            var result = await base.PolinateAsync();

            _logger.LogInformation("Best solution. Func = " + result.CountFunction(Solution.Current));
            _logger.LogInformation("Values = " + result.ToString(Solution.Current));

            return result;
        }

        protected override async Task PolinateOnceAsync()
        {
            _logger.LogDebug("Start new step " + _step);

            await base.PolinateOnceAsync();

            foreach (var group in Groups)
            {
                await FindBestSolutionAsync(group);
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

        protected override async Task GoFirstBranchAsync(PollinatorsGroup group, Pollinator pollinator)
        {
            _logger.LogTrace("Pollinator " + group.IndexOf(pollinator) + " goes first branch");
            await base.GoFirstBranchAsync(group, pollinator);
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

        protected async Task FindBestSolutionAsync(PollinatorsGroup group)
        {
            var result = await Mediator.Send(new BestSolutionRequest(new[] { group }, AlgorithmSettings.IsMin));

            _logger.LogDebug("Group. Local minimum. Func = "
                + result.CountFunction(Solution.Current));

            _logger.LogDebug("Values = " + result.ToString(Solution.Current));
        }
    }
}
