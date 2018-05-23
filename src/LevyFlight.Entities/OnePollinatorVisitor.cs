using System;

namespace LevyFlight.Entities
{
    public class OnePollinatorVisitor
    {
        private readonly Func<double[], double> _function;

        public OnePollinatorVisitor(Func<double[], double> function)
        {
            _function = function;
        }

        public double Visit(Pollinator pollinator)
        {
            return _function(pollinator.Values);
        }
    }
}
