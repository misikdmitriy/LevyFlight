namespace LevyFlightSharp.DependencyInjection
{
    public class InjectionNames
    {
        // Facades
        public const string AckleyFunctionFacadeName = "AckleyFunctionFacade";
        public const string GriewankFunctionFacadeName = "GriewankFunctionFacade";
        public const string RastriginFunctionFacadeName = "RastriginFunctionFacade";
        public const string RosenbrockFunctionFacadeName = "RosenbrockFunctionFacade";
        public const string SphereFunctionFacadeName = "SphereFunctionFacade";

        public const string MainFunctionFacadeName = GriewankFunctionFacadeName;

        // Algorithms
        public const string LevyFlightAlgorithmName = "LevyFlightAlgorithm";
        public const string LevyFlightAlgorithmLoggerName = "LevyFlightAlgorithmLogger";

        public const string LevyFlightAlgorithmMainName = LevyFlightAlgorithmLoggerName;
    }
}
