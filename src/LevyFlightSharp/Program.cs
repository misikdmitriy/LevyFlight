using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Facade;
using LevyFlightSharp.Services;
using LevyFlightSharp.Strategies;

namespace LevyFlightSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mantegnaFunctionStrategy = new MantegnaFunctionStrategy();
            var mainFunctionStrategy = new GriewankFunctionStrategy();
            var functionFacade = new FunctionFacade(mainFunctionStrategy, mantegnaFunctionStrategy);

            var algorithm = new AlgorithmProxy(functionFacade);
            var timer = new TimeCounter();

            timer.Start();
            algorithm.Polinate();
            timer.End();
        }
    }
}
