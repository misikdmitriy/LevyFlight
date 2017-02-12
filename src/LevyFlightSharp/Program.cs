using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Facade;
using LevyFlightSharp.Services;

namespace LevyFlightSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Mediator.Mediator.Register();

            var functionFacade = new GriewankFunctionFacade();

            var algorithm = new LevyFlightAlgorithmLogger(functionFacade);
            var timer = new TimeCounter();

            timer.Start();
            algorithm.Polinate();
            timer.End();
        }
    }
}
