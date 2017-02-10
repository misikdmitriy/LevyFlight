using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Services;
using LevyFlightSharp.Strategies;

namespace LevyFlightSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var functionsStrategy = new MantegnaFunctionStrategy();
            var mainFunctionStrategy = new GriewankFunctionStrategy();

            var algorithm = new AlgorithmProxy(mainFunctionStrategy, functionsStrategy);
            var timer = new TimeCounter();

            timer.Start();
            algorithm.Polinate();
            timer.End();
        }
    }
}
