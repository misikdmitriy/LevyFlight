using System;

namespace LevyFlight.Common.Misc
{
    public class RandomGenerator
    {
        public static Random Random => new Random((int)DateTime.Now.Ticks);
    }
}
