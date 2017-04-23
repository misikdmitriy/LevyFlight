using LevyFlight.Strategies;

namespace LevyFlight.Facade
{
    public class FunctionFacade
    {
        public IFunctionStrategy<double, double[]> MainFunctionStrategy { get; }
        public IFunctionStrategy<double, double> MantegnaFunctionStrategy { get; }
        public IFunctionStrategy<double, double> LambdaFunctionStrategy { get; }
        public IFunctionStrategy<double, double[]> DistanceFunctionStrategy { get; }

        public FunctionFacade(IFunctionStrategy<double, double[]> mainFunctionStrategy, 
            IFunctionStrategy<double, double> mantegnaFunctionStrategy, 
            IFunctionStrategy<double, double> lambdaFunctionStrategy, 
            IFunctionStrategy<double, double[]> distanceFunctionStrategy)
        {
            MainFunctionStrategy = mainFunctionStrategy;
            MantegnaFunctionStrategy = mantegnaFunctionStrategy;
            LambdaFunctionStrategy = lambdaFunctionStrategy;
            DistanceFunctionStrategy = distanceFunctionStrategy;
        }
    }
}
