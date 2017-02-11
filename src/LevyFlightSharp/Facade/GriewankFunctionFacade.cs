using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class GriewankFunctionFacade : FunctionFacade
    {
        public GriewankFunctionFacade() 
            : base(new GriewankFunctionStrategy(), new MantegnaFunctionStrategy())
        {
        }
    }
}
