using Autofac;

using LevyFlight.Algorithms;
using LevyFlight.DependencyInjection;
using LevyFlight.Facade;

namespace LevyFlight.Services
{
    public class AppNameResolver
    {
        public static LevyFlightAlgorithm ToAlgorithm(bool useLogger, string functionName)
        {
            return DependencyRegistration.Container
                .ResolveNamed<LevyFlightAlgorithm>(ToAlgorithmName(useLogger),
                    new NamedParameter("functionFacade", ToFunctionFacade(functionName)));
        }

        public static FunctionFacade ToFunctionFacade(string functionName)
        {
            return DependencyRegistration.Container
                .ResolveNamed<FunctionFacade>(ToFacadeName(functionName));
        }

        public static string ToAlgorithmName(bool useLogger)
        {
            return useLogger
                ? InjectionNames.LevyFlightAlgorithmLoggerName
                : InjectionNames.LevyFlightAlgorithmName;
        }

        public static string ToFacadeName(string functionName)
        {
            return $"{functionName}Facade";
        }
    }
}
