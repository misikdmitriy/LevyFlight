using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class RastriginFunctionFacade : FunctionFacade
    {
        public RastriginFunctionFacade() 
            : base(new RastriginFunctionStrategy(), new MantegnaFunctionStrategy())
        {
        }
    }
}
