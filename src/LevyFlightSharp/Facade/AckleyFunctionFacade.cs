using LevyFlightSharp.Strategies;

namespace LevyFlightSharp.Facade
{
    public class AckleyFunctionFacade : CommonFunctionFacade
    {
        public AckleyFunctionFacade() 
            : base(new AckleyFunctionStrategy())
        {
        }
    }
}
