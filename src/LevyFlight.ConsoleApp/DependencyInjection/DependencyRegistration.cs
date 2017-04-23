using System.Collections.Generic;
using System.Reflection;

using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;

using LevyFlight.Algorithms;
using LevyFlight.Facade;
using LevyFlight.MediatorRequests;
using LevyFlight.Services;
using LevyFlight.Strategies;

using MediatR;

namespace LevyFlight.ConsoleApp.DependencyInjection
{
    public class DependencyRegistration
    {
        public static IContainer Container { get; private set; }

        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Enables contravariant Resolve() for interfaces with single contravariant ("in") arg
            builder
              .RegisterSource(new ContravariantRegistrationSource());

            // Mediator
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            // Request handlers
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => { object o; return c.TryResolve(t, out o) ? o : null; };
            });

            // notification handlers
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            // Register custom code
            builder.RegisterAssemblyTypes(typeof(BestSolutionRequestHandler).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces(); // via assembly scan

            // Facades registration

            builder.RegisterType<FunctionFacade>()
                .Named<FunctionFacade>(InjectionNames.AckleyFunctionFacadeName)
                .WithParameter("mainFunctionStrategy", new AckleyFunctionStrategy())
                .WithParameter("mantegnaFunctionStrategy", new MantegnaFunctionStrategy())
                .WithParameter("lambdaFunctionStrategy", new LambdaFunctionStrategy())
                .WithParameter("distanceFunctionStrategy", new DistanceFunctionStrategy());

            builder.RegisterType<FunctionFacade>()
                .Named<FunctionFacade>(InjectionNames.GriewankFunctionFacadeName)
                .WithParameter("mainFunctionStrategy", new GriewankFunctionStrategy())
                .WithParameter("mantegnaFunctionStrategy", new MantegnaFunctionStrategy())
                .WithParameter("lambdaFunctionStrategy", new LambdaFunctionStrategy())
                .WithParameter("distanceFunctionStrategy", new DistanceFunctionStrategy());

            builder.RegisterType<FunctionFacade>()
                .Named<FunctionFacade>(InjectionNames.RastriginFunctionFacadeName)
                .WithParameter("mainFunctionStrategy", new RastriginFunctionStrategy())
                .WithParameter("mantegnaFunctionStrategy", new MantegnaFunctionStrategy())
                .WithParameter("lambdaFunctionStrategy", new LambdaFunctionStrategy())
                .WithParameter("distanceFunctionStrategy", new DistanceFunctionStrategy());

            builder.RegisterType<FunctionFacade>()
                .Named<FunctionFacade>(InjectionNames.RosenbrockFunctionFacadeName)
                .WithParameter("mainFunctionStrategy", new RosenbrockFunctionStrategy())
                .WithParameter("mantegnaFunctionStrategy", new MantegnaFunctionStrategy())
                .WithParameter("lambdaFunctionStrategy", new LambdaFunctionStrategy())
                .WithParameter("distanceFunctionStrategy", new DistanceFunctionStrategy());

            builder.RegisterType<FunctionFacade>()
                .Named<FunctionFacade>(InjectionNames.SphereFunctionFacadeName)
                .WithParameter("mainFunctionStrategy", new SphereFunctionStrategy())
                .WithParameter("mantegnaFunctionStrategy", new MantegnaFunctionStrategy())
                .WithParameter("lambdaFunctionStrategy", new LambdaFunctionStrategy())
                .WithParameter("distanceFunctionStrategy", new DistanceFunctionStrategy());

            // Algorithms registrations

            builder.RegisterType<LevyFlightAlgorithm>()
                .Named<LevyFlightAlgorithm>(InjectionNames.LevyFlightAlgorithmName)
                .WithParameter("algorithmSettings", ConfigurationService.AppSettings.AlgorithmSettings)
                .WithParameter(new ResolvedParameter(
                    (info, context) => info.Name == "mediator", 
                    (info, context) => context.Resolve<IMediator>()));

            builder.RegisterType<LevyFlightAlgorithmLogger>()
                .Named<LevyFlightAlgorithm>(InjectionNames.LevyFlightAlgorithmLoggerName)
                .WithParameter("algorithmSettings", ConfigurationService.AppSettings.AlgorithmSettings)
                .WithParameter(new ResolvedParameter(
                    (info, context) => info.Name == "mediator",
                    (info, context) => context.Resolve<IMediator>()));

            builder.RegisterType<PollinatorAnalyzer>()
                .AsSelf();

            // Container

            Container = builder.Build();
        }
    }
}
