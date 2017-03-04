using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class RosenbrockFunctionFacade : CommonFunctionFacade
    {
        public RosenbrockFunctionFacade() 
            : base(new RosenbrockFunctionStrategy())
        {
        }
    }
}
