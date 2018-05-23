using System;
using LevyFlight.Entities;

namespace LevyFlight.Extensions
{
    public static class PollinatorExtensions
    {
        public static double CountFunction(this Pollinator pollinator, Func<double[], double> functionStrategy)
        {
            return new OnePollinatorVisitor(functionStrategy).Visit(pollinator);
        }

        public static void ThrowExceptionIfValuesIncorrect(this Pollinator pollinator)
        {
            pollinator.CountFunction(doubles =>
            {
                foreach (var element in doubles)
                {
                    if (double.IsNaN(element))
                    {
                        throw new ArithmeticException("Got NaN");
                    }

                    if (double.IsInfinity(element))
                    {
                        throw new ArithmeticException("Got infinity");
                    }
                }

                return 0.0;
            });
        }

        public static int CompareTo(this Pollinator first, Pollinator second, Func<double[], double> functionStrategy)
        {
            var firstSolution = first.CountFunction(functionStrategy);
            var secondSolution = second.CountFunction(functionStrategy);

            return firstSolution < secondSolution ? -1 : (firstSolution > secondSolution ? 1 : 0);
        }
    }
}
