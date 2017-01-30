using System;
using System.Linq;
using LevyFlightSharp.Algorithms;
using LevyFlightSharp.Services;

namespace LevyFlightSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var algorithm = new AlgorithmProxy(RastriginFunction);
            var timer = new TimeCounter();

            timer.Start();
            algorithm.Polinate();
            timer.End();
        }

        private static double GriewankFunction(double[] flowers)
        {
            var sum1 = 0.0;
            var sum2 = 1.0;

            var num = 1;
            foreach (var x in flowers)
            {
                sum1 += x * x / 4000.0;
                sum2 *= Math.Cos(x / Math.Sqrt(num));
                ++num;
            }

            return sum1 - sum2 + 1.0;
        }

        private static double RastriginFunction(double[] flowers)
        {
            var a = 10.0;
            var an = flowers.Length * a;

            var sum1 = flowers.Sum(x => x * x - a * Math.Cos(2 * Math.PI * x));

            return an + sum1;
        }
    }
}
