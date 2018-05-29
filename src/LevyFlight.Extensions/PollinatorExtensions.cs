using System;
using System.Linq;
using LevyFlight.Entities;

namespace LevyFlight.Extensions
{
    public static class PollinatorExtensions
    {
        public static double CountFunction(this Pollinator pollinator, Func<double[], double> functionStrategy)
        {
            return functionStrategy(pollinator.ToArray());
        }

        public static bool CheckWhetherValuesCorrect(this Pollinator pollinator)
        {
            foreach (var element in pollinator)
            {
                if (double.IsNaN(element))
                {
                    return false;
                }

                if (double.IsInfinity(element))
                {
                    return false;
                }
            }

            return true;
        }

        public static void ThrowExceptionIfValuesIncorrect(this Pollinator pollinator)
        {
            if (!pollinator.CheckWhetherValuesCorrect())
            {
                throw new ArgumentException("Some values are NaN or +/- Infinity");
            }
        }

        public static string ToArrayRepresentation(this Pollinator pollinator)
        {
            return string.Join(";", pollinator);
        }
    }
}
