using System;
using LevyFlight.Common.Check;

namespace LevyFlight.Entities
{
    public class Pollinator
    {
        public const double P = 0.01;

        public int Size { get; }

        public double[] CurrentSolution { get; set; }
        public double[] NewSolution { get; set; }

        public Pollinator(int size)
        {
            ExceptionHandler.ThrowExceptionIfNegativeOrZero(size, nameof(size));

            Size = size;
        }

        public string ToString(Solution solution)
        {
            switch (solution)
            {
                case Solution.Current:
                    return ToString(CurrentSolution);
                case Solution.NewSolution:
                    return ToString(NewSolution);
                default:
                    throw new ArgumentOutOfRangeException(nameof(solution), solution, null);
            }
        }

        private string ToString(double[] pollinator)
        {
            var result = "";
            for (var i = 0; i < Size; i++)
            {
                result += $"{i}): {pollinator[i]:e2}; ";
            }
            return result;
        }
    }

    
}