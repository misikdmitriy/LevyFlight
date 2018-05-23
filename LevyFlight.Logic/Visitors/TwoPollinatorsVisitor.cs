using System;
using LevyFlight.Entities;

namespace LevyFlight.Logic.Visitors
{
    public class TwoPollinatorsVisitor
    {
        private readonly Func<double[], double[], double[]> _function;

        public TwoPollinatorsVisitor(Func<double[], double[], double[]> function)
        {
            _function = function;
        }

        public double[] Visit(Pollinator first, Pollinator second)
        {
            return _function(first.Values, second.Values);
        }
    }
}
