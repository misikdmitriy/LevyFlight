using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LevyFlightSharp.Mediator
{
    public class Mediator
    {
        private static IDictionary<Type, Type> RequestResolver { get; }
        public static Mediator Instance { get; }

        static Mediator()
        {
            RequestResolver = new Dictionary<Type, Type>();
            Instance = new Mediator();
        }

        protected Mediator()
        {
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request, params object[] arguments)
        {
            var type = RequestResolver[request.GetType()];

            var requestHandler = Activator.CreateInstance(type, arguments);
            var method = type.GetMethod("Handle");
            var res = (TResponse)method.Invoke(requestHandler, new object[] { request });

            return res;
        }

        public static void Register(Type requestHandlerType)
        {
            RequestResolver[requestHandlerType.GetInterfaces()
                .Single(i => i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .GetGenericArguments().First()] = requestHandlerType;
        }

        public static void Register()
        {
            Register(typeof(BestSolutionRequestHandler));
        }
    }

    public interface IRequest<TResponse>
    {
    }

    public interface IRequestHandler<in TRequest, out TResponse>
        where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest request);
    }
}
