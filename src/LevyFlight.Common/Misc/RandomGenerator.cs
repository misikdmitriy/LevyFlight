using System;

namespace LevyFlight.Common.Misc
{
    public class RandomGenerator
    {
        public static Random Random { get; }

        static RandomGenerator()
        {
            Random = new Random((int)DateTime.Now.Ticks);
        }
    }

    public static class RandomExtensions
    {
        public static double NextGaussian(this Random rand, double mean, double stdDev)
        {
            var u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            var u2 = 1.0 - rand.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * 
                Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            var randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;
        }
    }
}
