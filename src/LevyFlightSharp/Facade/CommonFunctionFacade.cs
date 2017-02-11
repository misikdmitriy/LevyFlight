using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class CommonFunctionFacade : FunctionFacade
    {
        public CommonFunctionFacade(IFunctionStrategy<double, double[]> mainFunctionStrategy) 
            : base(mainFunctionStrategy, 
                  new MantegnaFunctionStrategy(), 
                  new LambdaFunctionStrategy(), 
                  new DistanceFunctionStrategy())
        {
        }
    }
}
