namespace LevyFlightSharp.Strategies
{
    public interface IFunctionStrategy<out TResult, in TArguments>
    {
        TResult Function(TArguments arguments);
    }
}
