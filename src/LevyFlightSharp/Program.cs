using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Facade;
using LevyFlightSharp.Mediator;
using LevyFlightSharp.Services;

namespace LevyFlightSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Mediator.Mediator.Register(typeof(BestSolutionRequestHandler));

            var functionFacade = new GriewankFunctionFacade();

            var algorithm = new AlgorithmProxy(functionFacade);
            var timer = new TimeCounter();

            timer.Start();
            algorithm.Polinate();
            timer.End();
        }
    }
}
