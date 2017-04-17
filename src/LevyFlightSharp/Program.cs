using Autofac;

using LevyFlightSharp.Algorithms;
using LevyFlightSharp.DependencyInjection;
using LevyFlightSharp.Facade;
using LevyFlightSharp.Services;

namespace LevyFlightSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DependencyRegistration.Register();

            var facade = DependencyRegistration.Container
                .ResolveNamed<FunctionFacade>(InjectionNames.MainFunctionFacadeName);

            var algorithm = DependencyRegistration.Container
                .ResolveNamed<LevyFlightAlgorithm>(InjectionNames.LevyFlightAlgorithmMainName,
                    new NamedParameter("functionFacade", facade));

            var timer = new TimeCounter();

            timer.Start();
            algorithm.PolinateAsync().Wait();
            timer.End();
        }
    }
}
