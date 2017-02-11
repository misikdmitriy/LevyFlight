using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class FunctionFacade
    {
        public IFunctionStrategy<double, double[]> MainFunctionStrategy { get; }
        public IFunctionStrategy<double, double> MantegnaFunctionStrategy { get; }

        public FunctionFacade(IFunctionStrategy<double, double[]> mainFunctionStrategy, 
            IFunctionStrategy<double, double> mantegnaFunctionStrategy)
        {
            MainFunctionStrategy = mainFunctionStrategy;
            MantegnaFunctionStrategy = mantegnaFunctionStrategy;
        }
    }
}
