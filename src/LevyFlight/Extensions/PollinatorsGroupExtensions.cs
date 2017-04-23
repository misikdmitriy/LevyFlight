using System;
using System.Linq;

using LevyFlight.Entities;
using LevyFlight.Services;

namespace LevyFlight.Extensions
{
    public static class PollinatorsGroupExtensions
    {
        public static int IndexOf(this PollinatorsGroup group, Pollinator pollinator)
        {
            return Array.IndexOf(group.ToArray(), pollinator);
        }
    }
}
