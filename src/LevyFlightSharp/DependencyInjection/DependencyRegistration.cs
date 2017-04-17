﻿using System.Collections.Generic;
using System.Reflection;

using Autofac;
using Autofac.Features.Variance;

using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Facade;
using LevyFlightSharp.Strategies;

using MediatR;

namespace LevyFlightSharp.DependencyInjection
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
            }).InstancePerLifetimeScope();

            // notification handlers
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            }).InstancePerLifetimeScope();

            // Register custom code
            builder.RegisterAssemblyTypes(typeof(DependencyRegistration).GetTypeInfo().Assembly)
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
                .Named<LevyFlightAlgorithm>(InjectionNames.LevyFlightAlgorithmName);

            builder.RegisterType<LevyFlightAlgorithmLogger>()
                .Named<LevyFlightAlgorithm>(InjectionNames.LevyFlightAlgorithmLoggerName);

            builder.RegisterType<PollinatorAnalyzer>()
                .AsSelf();

            // Container

            Container = builder.Build();
        }
    }
}
