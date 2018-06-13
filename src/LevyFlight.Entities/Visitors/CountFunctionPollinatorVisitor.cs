using System;
using System.Linq;
using LevyFlight.Entities.Contracts;

namespace LevyFlight.Entities.Visitors
{
    public class CountFunctionPollinatorVisitor : IPollinatorVisitor<double>
    {
        private readonly Func<double[], double> _func;

        public CountFunctionPollinatorVisitor(Func<double[], double> func)
        {
            _func = func;
        }

        public double Visit(Pollinator pollinator)
        {
            return _func(pollinator.ToArray());
        }
    }
}
