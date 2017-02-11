using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class GriewankFunctionFacade : CommonFunctionFacade
    {
        public GriewankFunctionFacade() 
            : base(new GriewankFunctionStrategy())
        {
        }
    }
}
