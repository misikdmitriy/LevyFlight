using System;

namespace LevyFlightSharp
{
    public class RandomGenerator
    {
        public static Random Random { get; }

        static RandomGenerator()
        {
            Random = new Random((int)DateTime.Now.Ticks);
        }
    }
}
