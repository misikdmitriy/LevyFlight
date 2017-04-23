namespace LevyFlight.Strategies
{
    public interface IFunctionStrategy<out TResult, in TArguments>
    {
        TResult Apply(TArguments arguments);
    }
}
