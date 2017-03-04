using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class SphereFunctionFacade : CommonFunctionFacade
    {
        public SphereFunctionFacade() 
            : base(new SphereFunctionStrategy())
        {
        }
    }
}
