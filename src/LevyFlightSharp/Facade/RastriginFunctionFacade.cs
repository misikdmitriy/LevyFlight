using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class RastriginFunctionFacade : CommonFunctionFacade
    {
        public RastriginFunctionFacade() 
            : base(new RastriginFunctionStrategy())
        {
        }
    }
}
