using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public abstract class CommonFunctionFacade : FunctionFacade
    {
        protected CommonFunctionFacade(IFunctionStrategy<double, double[]> mainFunctionStrategy) 
            : base(mainFunctionStrategy, 
                  new MantegnaFunctionStrategy(), 
                  new LambdaFunctionStrategy(), 
                  new DistanceFunctionStrategy())
        {
        }
    }
}
